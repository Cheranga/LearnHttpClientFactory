namespace DTO.Requests
{
    public class GetUserRepositoriesRequest
    {
        public string UserName { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(UserName);
    }
}