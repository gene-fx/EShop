using Discount.Grpc;
using DiscountGrpc.Data;
using DiscountGrpc.Models;
using Google.Protobuf.Collections;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace DiscountGrpc.Services
{
    public class DiscountService(
        ILogger<DiscountService> logger, 
        DiscountContext dbContext)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                logger.LogError($"{DateTime.UtcNow} - No discount has been found for {request.ProductName}.");

                throw new RpcException(new Status(StatusCode.InvalidArgument, "No discount found"));
            }

            logger.LogInformation($"{DateTime.UtcNow} - Retrieving coupon {coupon.ProductName}");

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<GetAllDiscountsResponse> GetAllDiscounts(GetAllDiscountsResquest request, ServerCallContext context)
        {
            List<Coupon> discounts = await dbContext.Coupons.ToListAsync();

            var response = new GetAllDiscountsResponse();

            if (!discounts.Any())
            {
                response.List.Add("No product was found");

                logger.LogError($"{DateTime.UtcNow} - No coupons found");

                return response;
            }

            foreach (var discount in discounts)
            {
                response.List.Add(discount.ProductName);
            }

            logger.LogInformation($"{DateTime.UtcNow} - Getting all cupons");

            return response;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            if(await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName) is not null)
            {
                logger.LogError($"{DateTime.UtcNow} - Create Coupon error - coupon already registered \n {request.Coupon.ProductName}");
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{request.Coupon.ProductName} already registered"));
            }

            Coupon coupon = request.Coupon.Adapt<Coupon>();

            if(coupon is null)
            {
                logger.LogError($"{DateTime.UtcNow} - Create Coupon error - Invalid model");
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid model"));
            }

            var created = await dbContext.Coupons.AddAsync(coupon);

            if (created.State != EntityState.Added)
            {
                logger.LogError($"{DateTime.UtcNow} - Create Coupon error - Coupon couldn't be saved in Db");
                throw new RpcException(new Status(StatusCode.DataLoss, "Can't add discount to Db"));
            }

            await dbContext.SaveChangesAsync();

            return new CouponModel(request.Coupon);
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);

            if(coupon is null)
            {
                logger.LogError($"{DateTime.UtcNow} - Update Coupon error - Coupon couldn't be found");
                throw new RpcException(new Status(StatusCode.DataLoss, "Coupon couldn't be found"));
            }

            coupon.Amount = request.Coupon.Amount;
            coupon.Description = request.Coupon.Description;

            var update = dbContext.Coupons.Update(coupon!);

            if (update.State != EntityState.Modified)
            {
                logger.LogError($"{DateTime.UtcNow} - Update Coupon error - Coupon couldn't be updated");
                throw new RpcException(new Status(StatusCode.DataLoss, "Can't update discount to Db"));
            }

            await dbContext.SaveChangesAsync();

            return request.Coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            Coupon? coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
            {
                logger.LogError($"{DateTime.UtcNow} - Delete Coupon error - Coupon couldn't be found");
                throw new RpcException(new Status(StatusCode.DataLoss, "Coupon couldn't be found"));
            }

            var delete = dbContext.Coupons.Remove(coupon);

            if (delete.State != EntityState.Deleted)
            {
                logger.LogError($"{DateTime.UtcNow} - Delete Coupon error - Coupon couldn't be deleted from Db");
                throw new RpcException(new Status(StatusCode.DataLoss, "Coupon couldn't be deleted from Db"));
            }

            await dbContext.SaveChangesAsync();

            var success = new DeleteDiscountResponse();
            success.Success = true;

            return success;
        }
    }
}
