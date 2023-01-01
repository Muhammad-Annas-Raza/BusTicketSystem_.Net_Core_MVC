using Microsoft.EntityFrameworkCore;
using OnlineBusTicketReservationSystem.Interface;
using OnlineBusTicketReservationSystem.Models;
using System.Linq;

namespace OnlineBusTicketReservationSystem.Services
{
    public class Repository<T> : IRepository<T> where T : class
    {

        int flag = 0;
        //1st make context class obj;
        private MyContext db_context;
        //2nd make IDbSet a Generic table DbSet<>
        private DbSet<T> table_name;
        private readonly IConfiguration _config;
        public Repository(MyContext db_context, IConfiguration config)
        //public Repository(db_POS db_context)

        {
            this.db_context = db_context;
            this.table_name = db_context.Set<T>();
            this._config = config;

        }

        public async Task<int> Create(T obj)
        {
            await db_context.AddAsync(obj);
            int a = await SaveMyChanges();
            if (a>0)
            {
                return a;
            }
            else
            {
                return -1;
            }
        }

        public async Task<List<T>> GetAllRows()
        {
          List < T >  rows = await table_name.ToListAsync();
            return rows;
        }

        public async Task<T?> GetRowById(long id)
        {
           T? row = await table_name.FindAsync(id);
            return row;
        }

        public async Task<int> Update(T row)
        {
            if (row != null)
            {
                db_context.Entry(row).State = EntityState.Modified;
                int a = await SaveMyChanges();
                if (a>0)
                {
                    return a;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public async Task<int> Delete(long id)
        {
            T? row =await GetRowById(id);
            if (row != null)
            {
                db_context.Entry(row).State = EntityState.Deleted;
                int a = await SaveMyChanges();
                if (a>0)
                {
                    return a;

                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return -1;
            }

        }

        public async Task<int> SaveMyChanges()
        {
            return await db_context.SaveChangesAsync();
        }
        public async Task<tbl_user> ChkCredentials(string usrEmlPhn, string pwd)
        {
           tbl_user? row =await db_context.tbl_user.FirstOrDefaultAsync(m => m.user_email_phone == usrEmlPhn && m.user_password == pwd);
            if (row != null)
            {
                return row;
            }
            return null;
        }
        public async Task<string> VerifyCode(long id,string VerificationCode)
        {
            tbl_user? row = await db_context.tbl_user.FindAsync(id);
            if (row != null)
            {
                if (row.user_verification_code == VerificationCode)
                {
                    row.user_emailVerified = true;
                    db_context.Entry(row).State = EntityState.Modified;
                    flag =await db_context.SaveChangesAsync();
                    if (flag>0)
                    {
                        if (row.user_approved == true)
                        {
                            return "true";
                        }
                        else
                        {
                            return "Verified but not approved by user";
                        }
                    }
                    else
                    {
                        return "Failed to update in database";
                    }
                }
                else
                {
                    return "Verification Code is Incorrect";
                }

            }
            else
            {                    
                return "Data not found";
            }
            
        }
        public async Task<List<tbl_user>> GetAllSpecificConductors(long id)
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_id_ForConductor == id).ToListAsync();
            return rows;
        }
        public async Task<List<tbl_user>> GetAllUnApprovedBusOwners()
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_role == "Bus Owner" && m.user_approved == false).ToListAsync();
            return rows;
        }
        public async Task<List<tbl_user>> GetAllConductors(long id)
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_role == "Conductor" && m.user_id_ForConductor == id).ToListAsync();
            return rows;
        }
        
        public async Task<List<tbl_user>> GetAllApprovedBusOwners()
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_role == "Bus Owner" && m.user_approved == true).ToListAsync();
            return rows;
        }
        public async Task<int> ApproveBusOwner(long id)
        {
            tbl_user? row = await db_context.tbl_user.FindAsync(id);
            if (row != null)
            {
                row.user_approved = true;
                db_context.Entry(row).State = EntityState.Modified;
                flag = await db_context.SaveChangesAsync();

                return flag > 0 ? flag : -1;
            }
            return -1;
        }
        public async Task<List<tbl_bus>> GetBusesfk_user_id(long fk_user_id)
        {
            List<tbl_bus> rows = await db_context.tbl_bus.Where(m => m.fk_user_id == fk_user_id).ToListAsync();
            return rows;
        }


        public async Task<List<tbl_discount>> GetDiscountInnerJoin(long id)
        {

           var xyz =  await (from a in db_context.tbl_bus
            join b in db_context.tbl_discount
            on a.bus_id equals b.fk_bus_id
            where a.fk_user_id == id
            select new
            {
                discount_id = b.discount_id,
                discount_0_TO_5 = b.discount_0_TO_5,
                discount_6_TO_12   = b.discount_6_TO_12,          
                discount_13_TO_50  = b.discount_13_TO_50,           
                discount_51  = b.discount_51,
                Created_at  = b.Created_at,
                fk_bus_id  = b.fk_bus_id,
                bus_number = a.bus_NumberPlate
                 
            }).ToListAsync();

            List<tbl_discount> lst = new List<tbl_discount>();
            foreach (var item in xyz)
            {
                lst.Add(new tbl_discount()
                {
                    discount_id = item.discount_id,
                    discount_0_TO_5=item.discount_0_TO_5,
                    discount_6_TO_12=item.discount_6_TO_12,
                    discount_13_TO_50=item.discount_13_TO_50,
                    discount_51=item.discount_51,
                    Created_at=item.Created_at,
                    fk_bus_id = item.fk_bus_id,
                    bus_number = item.bus_number
                });
            }

            return lst;
            
        }
        public async Task<tbl_bus> GetBusForConductor(string NumberPlate)
        {
            tbl_bus? row = await db_context.tbl_bus.FirstOrDefaultAsync(m => m.bus_NumberPlate == NumberPlate);
            return row;
        }
        public async Task<List<tbl_busSeats>> CollectAmount(long fk_bus_id)
        {
            List<tbl_busSeats> rows = await db_context.tbl_busSeats.Where(m => m.fk_bus_id == fk_bus_id && m.busSeat_customerReachedAmountCollected == false).ToListAsync();
            return rows;
        } 
        public async Task<List<tbl_busSeats>> CollectedAmount(long fk_bus_id)
        {
            List<tbl_busSeats> rows = await db_context.tbl_busSeats.Where(m => m.fk_bus_id == fk_bus_id && m.busSeat_customerReachedAmountCollected == true).ToListAsync();
            return rows;
        } 
        public async Task<int> DeleteConductorByBusNumber(string BusNumber) 
        {
            tbl_user row = await db_context.tbl_user.FirstOrDefaultAsync(m => m.bus_NumberPlateForConductor == BusNumber);
             db_context.Entry(row).State = EntityState.Deleted;
            flag = db_context.SaveChanges();
            
            return flag;
        } 
        public async Task<List<tbl_bus>> GetExpressBus()
        {
            return await db_context.tbl_bus.Where(m => m.bus_category == "Express" && m.bus_available == true).ToListAsync();
        }
         
        public async Task<List<tbl_bus>> GetLuxuryBus()
        {
            return await db_context.tbl_bus.Where(m => m.bus_category == "Luxury" && m.bus_available == true).ToListAsync();
        }
       
         
        public async Task<List<tbl_bus>> GetVolvoACBus()
        {
            return await db_context.tbl_bus.Where(m => m.bus_category == "Volvo (A/C)" && m.bus_available == true).ToListAsync();
        }
         
        public async Task<List<tbl_bus>> GetVolvoNonACBus()
        {
            return await db_context.tbl_bus.Where(m => m.bus_category == "Volvo (Non A/C)" && m.bus_available == true).ToListAsync();
        }
        public async Task<List<tbl_busSeats>> GetBusSeats(long id)
        {
            return await db_context.tbl_busSeats.Where(m => m.fk_bus_id == id).ToListAsync();
        }
        
        public async Task<List<tbl_history>> GetUserHistory(long id)
        {
            return await db_context.tbl_history.Where(m => m.fk_user_id == id).OrderByDescending(m => m.history_id).ToListAsync();
        }

        public async Task<List<tbl_bus>> GetAvailableBus(long id)
        {
            return await db_context.tbl_bus.Where(m => m.bus_available == true  && m.fk_user_id == id).ToListAsync();
        }
        
        public async Task<List<tbl_sale>> GetSale(long id)
        {
            return await db_context.tbl_sale.Where(m => m.fk_user_id == id).ToListAsync();
        }

        public async Task<List<tbl_feedback>> GetFeedback()
        {
            return await db_context.tbl_feedback.OrderByDescending(m => m.feedback_id).ToListAsync();
        }

    }
}
