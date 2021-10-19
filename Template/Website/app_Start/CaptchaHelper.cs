using BotDetect;
using BotDetect.Web.Mvc;

namespace Website
{
    public static class CaptchaHelper
    {
        public static MvcCaptcha GetLoginCaptcha()
        {
            return new MvcCaptcha("LoginCaptcha")
            {
                UserInputID = "CaptchaCode",
                ImageFormat = ImageFormat.Jpeg,
            };
        }
    }
}
