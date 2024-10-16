using KoiDeliveryOrderingSystem.Data.Models;
using KoiDeliveryOrderingSystem.Data.Repository;

namespace KoiDeliveryOrderingSystem.Data
{
    public class UnitOfWork
    {
        private FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext context;
        private ShipmentTrackingRepository shipmentTrackingRepository;
        private ShipperRepository shipperRepository;
        private HealCheckRepository healCheckRepository;
        private PackagingProcessRepository packagingProcessRepository;
        private PricingPolicyRepository pricingPolicyRepository;
        private ShipmentOrderDetailRepository shipmentOrderDetailRepository;
        private ShipmentOrderRepository shipmentOrderRepository;
        private UserRepository userRepository;
        private AnimalTypeRepository animalTypeRepository;

        public UnitOfWork()
        {
            context ??= new FA24_SE1717_PRN231_G1_KoiDeliveryOrderingSystemContext();
        }

        public ShipmentTrackingRepository ShipmentTrackingRepository => shipmentTrackingRepository ??= new ShipmentTrackingRepository(context);
        public ShipperRepository ShipperRepository => shipperRepository ??= new ShipperRepository(context);
        public ShipmentOrderRepository ShipmentOrderRepository => shipmentOrderRepository ??= new ShipmentOrderRepository(context);
        public UserRepository UserRepository => userRepository ??= new UserRepository(context);
        public AnimalTypeRepository AnimalTypeRepository => animalTypeRepository ??= new AnimalTypeRepository(context);

        public HealCheckRepository HealCheckRepository
        {
            get { return healCheckRepository ??= new HealCheckRepository(); }
        }
        public PackagingProcessRepository PackagingProcessRepository => packagingProcessRepository ??= new PackagingProcessRepository(context);
        public PricingPolicyRepository PricingPolicyRepository => pricingPolicyRepository ??= new PricingPolicyRepository(context);
        public ShipmentOrderDetailRepository ShipmentOrderDetailRepository => shipmentOrderDetailRepository ??= new ShipmentOrderDetailRepository(context);

        ////TO-DO CODE HERE/////////////////

        #region Set transaction isolation levels

        /*
        Read Uncommitted: The lowest level of isolation, allows transactions to read uncommitted data from other transactions. This can lead to dirty reads and other issues.

        Read Committed: Transactions can only read data that has been committed by other transactions. This level avoids dirty reads but can still experience other isolation problems.

        Repeatable Read: Transactions can only read data that was committed before their execution, and all reads are repeatable. This prevents dirty reads and non-repeatable reads, but may still experience phantom reads.

        Serializable: The highest level of isolation, ensuring that transactions are completely isolated from one another. This can lead to increased lock contention, potentially hurting performance.

        Snapshot: This isolation level uses row versioning to avoid locks, providing consistency without impeding concurrency. 
         */

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    result = context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            //System.Data.IsolationLevel.Snapshot
            using (Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    result = await context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        #endregion
    }
}
