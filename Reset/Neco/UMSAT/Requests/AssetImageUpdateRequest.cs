using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Neco.Common.Requests;

namespace Igtampe.Neco.Common.UMSAT.Requests {
    /// <summary>Request to update the image of an asset</summary>
    public class AssetImageUpdateRequest:INecoRequest {

        /// <summary>ID of the session executing this request</summary>
        public Guid SessionID { get; set; }

        /// <summary>ID of the Asset to add this image to</summary>
        public Guid AssetID { get; set; }

        /// <summary>Image as a byte array that should be a png</summary>
        public byte[] Image { get; set; }

    }
}
