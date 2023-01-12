namespace L2Rarity.Client.Models
{
    public interface IClipboardService
    {
        Task CopyToClipboard(string text);
    }
}
