using Camp.Interfaces;

namespace Web.Controllers
{
    public class CampEntityController : CampControllerBase
    {
        private readonly ICampDB _campDB;
        protected ICampDB CampDB
        {
            get { return _campDB; }
        }


		public CampEntityController(ICampDB campDB)
        {
            _campDB = campDB;
        }
    }
}
