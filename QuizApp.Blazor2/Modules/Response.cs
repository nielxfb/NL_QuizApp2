namespace QuizApp.Blazor2.Modules;

public class Response<T>
{
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
}