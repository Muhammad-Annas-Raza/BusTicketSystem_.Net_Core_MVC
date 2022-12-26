using OnlineBusTicketReservationSystem.Models;

namespace OnlineBusTicketReservationSystem.Interface
{
     
        public interface IRepository<T> where T : class
        {
        Task<int> Create(T obj);
        Task<List<T>> GetAllRows();
        Task<T?> GetRowById(long id);
        Task<int> Update(T row);
        Task<int> Delete(long id);
        Task<int> SaveMyChanges();
        Task<tbl_user> ChkCredentials(string usrNm, string pwd);
        Task<string> VerifyCode(long id, string VerificationCode);
        Task<List<tbl_user>> GetAllUnApprovedBusOwners();
        Task<List<tbl_user>> GetAllSpecificConductors(long id);
        Task<List<tbl_user>> GetAllApprovedBusOwners();
        Task<List<tbl_user>> GetAllConductors(long id);
        Task<int> ApproveBusOwner(long id);
        Task<List<tbl_bus>> GetBusesfk_user_id(long fk_user_id);
        Task<int> DeleteConductorByBusNumber(string BusNumber);
        Task<List<tbl_discount>> GetDiscountInnerJoin(long id);
        Task<tbl_bus> GetBusForConductor(string NumberPlate);
        Task<List<tbl_busSeats>> CollectAmount(long fk_bus_id);
        Task<List<tbl_busSeats>> CollectedAmount(long fk_bus_id);
        Task<List<tbl_bus>> GetExpressBus();
        Task<List<tbl_bus>> GetLuxuryBus();
        Task<List<tbl_bus>> GetVolvoACBus();
        Task<List<tbl_bus>> GetVolvoNonACBus();
        Task<List<tbl_busSeats>> GetBusSeats(long id);
        Task<List<tbl_history>> GetUserHistory(long id);

      
    }
}
