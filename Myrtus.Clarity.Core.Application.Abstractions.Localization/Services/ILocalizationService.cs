namespace Myrtus.Clarity.Core.Application.Abstractions.Localization.Services
{
    public interface ILocalizationService
    {
        string GetLocalizedString(string key, string language);
    }
}
