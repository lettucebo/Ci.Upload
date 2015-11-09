namespace Creatidea.Library.Uploads.Imgur
{
    using System.Diagnostics;
    using System.Web;

    using global::Imgur.API.Authentication.Impl;
    using global::Imgur.API.Endpoints.Impl;
    using global::Imgur.API.Exceptions;

    /// <summary>
    /// Class CiImgur.
    /// </summary>
    public class CiImgur
    {
        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CiImgur"/> class.
        /// </summary>
        public CiImgur()
        {
            // todo get setting form config
            this.Client = new ImgurClient("", "");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CiImgur"/> class.
        /// </summary>
        /// <param name="id">The client id.</param>
        /// <param name="secret">The client secret.</param>
        public CiImgur(string id, string secret)
        {
            this.Client = new ImgurClient(id, secret);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CiImgur"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="token">The token.</param>
        public CiImgur(string id, string secret, string token)
        {

        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the ImgurClient.
        /// </summary>
        /// <value>The ImgurClient.</value>
        public ImgurClient Client { get; set; }

        #endregion
    }
}
