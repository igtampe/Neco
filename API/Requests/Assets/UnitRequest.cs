using Igtampe.Neco.Common.Assets;

namespace Igtampe.Neco.API.Requests {
    
    /// <summary>Request relating to creating or updating a unit</summary>
    public class UnitRequest : AssetRequest<Unit> {

        //Actually, Units can be managed by just a standard asset request.
        //We'll leave this here in case Units eventually go beyond just a building they belong to, and standard asset data

        //BTW, that will be managed by route information rather than in the request.
        //Units will not be transferable between buildings. They're physically constructed somewhere. It makes no sense for them to be moved.

    }
}
