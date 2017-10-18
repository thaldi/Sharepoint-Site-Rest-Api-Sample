using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharepointRestSample
{
    public class ResponseModel
    {
        public class RootObject
        {
            public D d { get; set; }
        }
        public class D
        {
            public List<Result> results { get; set; }
        }

        public class Result
        {

            /// <summary>
            /// columns properties
            /// </summary>
            public bool VisibleOnDashboard { get; set; }
            public DateTime BeginDate { get; set; }
            public DateTime EndDate { get; set; }
            public string RedirectUrl { get; set; }
            public int ID { get; set; }
            public DateTime Created { get; set; }

            public string ImageUrl
            {
                get
                {
                    var sHelper = new ServiceHelper();
                    return sHelper.GetImageUrl(this.ID);
                }
            }
        }
    }


    public class ImageUrlResponseModel
    {
        public class Result
        {
            //image url property
            public string FileRef { get; set; }
        }

        public class D
        {
            public List<Result> results { get; set; }
        }

        public class RootObject
        {
            public D d { get; set; }
        }
    }


}
