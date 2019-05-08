using System;
namespace GCloudTransVisionAPI.Models
{
    public class TransImageClass
    {

        public String uri;
        public String from_translate;
        public String to_translate;

        public TransImageClass() {}
        
        public TransImageClass(String uri, String to_translate, String from_translate = "en")
        {
            this.uri = uri;
            this.from_translate = from_translate;
            this.to_translate = to_translate;
        }
    }
}
