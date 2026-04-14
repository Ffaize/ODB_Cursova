using WebAPI.Entities;
using WebAPI.Helpers;

namespace WebAPI.DataServices
{
    public class ActionLogService(ILogger<ActionLogService> logger)
    {
        public async Task<List<ActionLog>> GetAllActionLogs()
        {
            return await DbAccessService.GetItems<ActionLog>("sp_ActionLogs_GetAll");
        }

        public async Task<ActionLog?> GetActionLogById(Guid id)
        {
            return await DbAccessService.GetItemById<ActionLog>("sp_ActionLogs_GetById", id);
        }

        public async Task<bool> AddActionLog(ActionLog actionLog)
        {
            // Generate ID if it's empty
            if (actionLog.Id == Guid.Empty)
            {
                actionLog.Id = Guid.NewGuid();
            }
            
            // Set CreatedAt if not set
            if (actionLog.CreatedAt == default)
            {
                actionLog.CreatedAt = DateTime.UtcNow;
            }
            
            var rowsAffected = await DbAccessService.AddRecord("sp_ActionLogs_Add", actionLog);
            return rowsAffected > 0;
        }

        public async Task<bool> UpdateActionLog(ActionLog actionLog)
        {
            var rowsAffected = await DbAccessService.UpdateRecord("sp_ActionLogs_Update", actionLog);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteActionLog(Guid id)
        {
            var rowsAffected = await DbAccessService.DeleteRecord("sp_ActionLogs_Delete", id);
            return rowsAffected > 0;
        }

        public async Task<bool> AddMockData(int count = 1)
        {
            var actionLogs = Faker.GenerateMockActionLogs(count);
            foreach (var actionLog in actionLogs)
            {
                var success = await AddActionLog(actionLog);
                if (success) continue;
                logger.LogError("Failed to add mock action log with ID {ActionLogId}", actionLog.Id);
                return false;
            }
            return true;
        }
    }
}
