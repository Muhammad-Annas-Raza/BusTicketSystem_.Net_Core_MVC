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
           return await table_name.ToListAsync();
        }

        public async Task<T?> GetRowById(long id)
        {
            return await table_name.FindAsync(id);
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
        public async Task<List<tbl_user>> GetAllUnApprovedBusOwners()
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_role == "Bus Owner" && m.user_approved == false).ToListAsync();
            return rows;
        }
        public async Task<List<tbl_user>> GetAllConductors()
        {
            List<tbl_user> rows = await db_context.tbl_user.Where(m => m.user_role == "Conductor").ToListAsync();
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


        public async Task<List<tbl_discount>> GetDiscountInnerJoin()
        {

           var xyz =  await (from a in db_context.tbl_bus
            join b in db_context.tbl_discount
            on a.bus_id equals b.fk_bus_id

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





    }
}
