using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EFStories.Models
{
    public class BackgroundUploader
    {
        Cloudinary m_cloudinary;

        // Parameters of uploading tasks
        List<ImageUploadParamsExt> m_uploadParams;

        // Results of uploading tasks
        List<Image> m_images;

        // Background uploading task
        Task<bool> m_task;
        int m_progress = 0;
        object m_sync = new object();

        public List<Image> Images { get { return m_images; } }

        public Api CloudinaryApi { get { return m_cloudinary.Api; } }

        public BackgroundUploader(Cloudinary m_cloudinary)
        {
            this.m_cloudinary = m_cloudinary;
            // Check that application is properly installed in IIS
            string fileToCheck = HttpContext.Current.Server.MapPath("/Images/pizza.jpg");
            if (!File.Exists(fileToCheck))
                throw new ApplicationException(String.Format("Can't find file {0}!", fileToCheck));

            // Set up parameters of uploading tasks

            m_uploadParams = new List<ImageUploadParamsExt>();

            // Upload local image, public_id will be generated on Cloudinary's backend.
            m_uploadParams.Add(new ImageUploadParamsExt()
            {
                File = new FileDescription(HttpContext.Current.Server.MapPath("/Images/pizza.jpg")),
                Tags = "pizza tag",

                Caption = "The best pizza is the world",
                ShowTransform = new Transformation().Width(200).Height(150).Crop("fill")
            });
                      
            
            m_uploadParams.Add(new ImageUploadParamsExt()
            {
                File = new FileDescription("http://res.cloudinary.com/demo/image/upload/couple.jpg"),
                Tags = "basic_mvc4",
                Transformation = new Transformation().Width(500).Height(500).Crop("fit").Effect("saturation:-70"),

                Caption = "Young couple, Fill 200x150, round corners, apply the sepia effect",
                ShowTransform = new Transformation().Width(200).Height(150).Crop("fill").Gravity("face").Radius(10).Effect("sepia")
            });

            m_images = new List<Image>();

        }

        public int Progress { get { lock (m_sync) { return m_progress; } } }

        // Performs all uploading tasks and fills results (model to apply to view)
        public void Upload()
        {
            // Upload images in background to allow user to see the progress
            m_task = Task.Factory.StartNew(() =>
            {
                try
                {
                    for (int i = 0; i < m_uploadParams.Count; i++)
                    {
                        // Using Cloudinary API to upload images
                        ImageUploadResult result = m_cloudinary.Upload(m_uploadParams[i]);

                        m_images.Add(new Image()
                        {
                            // Copy predefined caption and transformation
                            Caption = m_uploadParams[i].Caption,
                            ShowTransform = m_uploadParams[i].ShowTransform,

                            // Load data from Cloudinary response
                            PublicId = result.PublicId,
                            Url = result.Uri.ToString(),
                            Format = result.Format
                        });

                        lock (m_sync)
                        {
                            m_progress = (i + 1) * 100 / m_uploadParams.Count;
                        }
                    }

                    return true;
                }
                catch
                {
                    lock (m_sync) { m_progress = 100; }

                    return false;
                }
            });
        }
    }
}