using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class BranchService(ILogger<BranchService> logger)
    {
        public async Task<List<Branch>> GetAllBranches()
        {
            return await DbAccessService.GetItems<Branch>("sp_Branches_GetAll");
        }

        public async Task<Branch?> GetBranchById(Guid id)
        {
            return await DbAccessService.GetItemById<Branch>("sp_Branches_GetById", id);
        }

        public async Task<bool> AddBranch(Branch branch)
        {
            var rowsAffected = await DbAccessService.AddRecord("sp_Branches_Add", branch);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateBranch(Branch branch)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_Branches_Update", branch);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteBranch(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_Branches_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var branches = Faker.GenerateMockBranches(count);
            foreach (var branch in branches)
            {
                var success = await AddBranch(branch);
                if (success) continue;
                logger.LogError("Failed to add mock branch with ID {BranchId}", branch.Id);
                return false;
            }
            return true;
        }
    }
}
