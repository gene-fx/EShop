namespace BuildingBlocks.Exceptions;
public class GenericExcepltionStringBuilder
{
    public static string BuildExceptionString(Exception ex)
    {
        var errorDetails = new System.Text.StringBuilder();
        errorDetails.AppendLine("ERROR MESSAGE: " + ex.Message);
        errorDetails.AppendLine("INNER EXCEPTION: " + ex.InnerException);
        errorDetails.AppendLine("STACK TRACE: " + ex.StackTrace);
        return errorDetails.ToString();
    }
}
