using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFStories.Models
{
    /// <summary>
    /// We can inherit API class to make it more convenient for us
    /// These fields will not be passes to cloudinary
    /// They will be used just at showing images after uploading
    /// </summary>
    public class ImageUploadParamsExt : ImageUploadParams
    {
        /// <summary>
        /// Image caption to show
        /// </summary>
        public string Caption;

    /// <summary>
    /// Transformation that will be applied at showing image, not at uploading
    /// </summary>
    public Transformation ShowTransform;
}
}