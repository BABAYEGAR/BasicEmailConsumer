namespace EmailConsumer.Models
{
    public class AppConfig
    {
        #region Cloudinary
        public string GeneralImage => "upload/w_640,h_670/w_640,l_watermark/";
        public string TableImage => "upload/w_60,h_80/w_50,l_watermark/";
        public string A5Image => "upload/w_559,h_794/";
        public string A4Image => "upload/w_794,h_1123/";
        public string A3Image => "upload/w_1123,h_1587/";
        public string A2Image => "upload/w_1587,h_2245/";
        public string A1Image => "upload/w_2245,h_3571/";


        #endregion

        #region SSO
        public string MarketPlaceBaseUrl => "https://camerack.com/";
        public long ClientId => 3;
        
        #endregion
        #region Mailer

        public string EmailServer => "smtp.gmail.com";
        public string Email => "support@camerack.com";
        public string Password => "Brigada95";
        public int Port => 465;
        public string NewUserHtml => "EmailTemplates/NewUser.html";
        public string NewUserSocialHtml => "EmailTemplates/NewUserSocial.html";
        public string ForgotPasswordHtml => "EmailTemplates/ForgotPassword.html";
        public string CompetitionHtml => "EmailTemplates/Competition.html";
        public string ImageRejectionHtml => "EmailTemplates/ImageRejectEmail.html";

        #endregion

        #region APP

        public string ProfilePicture => "wwwroot/UploadedImage/ProfilePicture/";

        public string ProfileBackgorundPicture => "wwwroot/UploadedImage/ProfileBackground/";
        public string ImageCategoryPicture => "wwwroot/UploadedImage/ImageCategory/";

        public string PhotoCategoryPicture => "wwwroot/UploadedImage/PhotoCategory/";
        #endregion

        #region Cloudinary

        public string CloudinaryAccoutnName => "cloudmab";
        public string CloudinaryApiKey => "988581656515289";
        public string CloudinaryApiSecret => "Odh29Eet7Ajilw0O0kCflwtnj9E";

        #endregion
    }
}
