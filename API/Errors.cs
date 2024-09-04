namespace API;

public class ApiException(int estatus, string message, string? details){

    public int Estatus { get; set; } = estatus;
    public string Message { get; set; } = message;

    public string? Details { get; set; } = details; 
}