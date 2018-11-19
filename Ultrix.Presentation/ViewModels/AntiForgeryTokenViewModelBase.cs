using Newtonsoft.Json;

namespace Ultrix.Presentation.ViewModels
{
    public abstract class AntiForgeryTokenViewModelBase
    {
        [JsonProperty("__RequestVerificationToken")]
        public string AntiForgeryToken { get; set; }
    }
}
