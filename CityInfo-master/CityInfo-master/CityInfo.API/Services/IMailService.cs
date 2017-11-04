namespace CityInfo.API.Services
{
    public interface IMailService
    {
        void Send(string to, string subject, string body);
    }
}