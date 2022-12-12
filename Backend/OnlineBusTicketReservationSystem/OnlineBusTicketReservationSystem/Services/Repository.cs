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

    }
}
