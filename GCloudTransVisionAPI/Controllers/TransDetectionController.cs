using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Google Cloud Imports
using Google.Cloud.Vision.V1;
using Google.Cloud.Translation.V2;
using RestSharp;
using GCloudTransVisionAPI.Models;

namespace GCloudTransVisionAPI.Controllers
{
    [Route("api/trans")]
    [ApiController]
    public class TransDetectionController : ControllerBase
    {
        public const String API_KEY = "AIzaSyDkJfD8K9mHKjIGwOuWnerVHjj8IQS3Z2U";
        public const String TRANSLATION_URL = "https://translation.googleapis.com/language/translate/v2?key=" + API_KEY;
        public const String VISION_URL = "https://vision.googleapis.com/v1/images:annotate?key=" + API_KEY;

        // POST api/trans
        [HttpPost]
        public IActionResult Post([FromBody] TransImageClass transImage)
        {
            String to_translate = "";

            // Cloud Vision API
            var image = Image.FromUri(transImage.uri);
            var client = ImageAnnotatorClient.Create();
            var response = client.DetectText(image);

            foreach (var annotation in response)
            {
                if (annotation.Description != null)
                    to_translate += annotation.Description + " ";
            }
            Console.WriteLine("\nText Detection Image \n --------------\n" + to_translate);

            // Translation API
            TranslationClient translation_client = TranslationClient.Create();
            var trans_response = translation_client.TranslateText(
                text: to_translate,
                targetLanguage: transImage.to_translate,  
                sourceLanguage: transImage.from_translate);

            String translated_text = trans_response.TranslatedText;

            Console.WriteLine("\nTranslation Output\n --------------\n" + translated_text);
            return StatusCode(200, translated_text);
        }
    }
}
