﻿using System;
using System.Collections.Generic;
using ESS.FW.Bpm.Engine.Impl.Core.Variable.Scope;
using ESS.FW.Bpm.Engine.Impl.Util;
using ESS.FW.Bpm.Engine.Persistence.Entity;

namespace ESS.FW.Bpm.Engine.Impl.Cmd
{
    

    /// <summary>
    ///     
    ///     
    /// </summary>
    [Serializable]
    public class RemoveTaskVariablesCmd : AbstractRemoveVariableCmd
    {
        private const long SerialVersionUid = 1L;

        public RemoveTaskVariablesCmd(string taskId, ICollection<string> variableNames, bool isLocal)
            : base(taskId, variableNames, isLocal)
        {
        }

        protected internal override ExecutionEntity ContextExecution
        {
            get
            {
                return GetEntity().GetExecution();//.Execution;
            }
        }

        protected TaskEntity GetEntity()
        {
            EnsureUtil.EnsureNotNull("taskId", EntityId);

            TaskEntity task = CommandContext.TaskManager.FindTaskById(EntityId);

            EnsureUtil.EnsureNotNull("Cannot find ITask with id " + EntityId, "ITask", task);

            CheckRemoveTaskVariables(task);

            return task;
        }
        protected internal override AbstractVariableScope Entity
        {
            get
            {
                EnsureUtil.EnsureNotNull("taskId", EntityId);

                TaskEntity task = CommandContext.TaskManager.FindTaskById(EntityId);

                EnsureUtil.EnsureNotNull("Cannot find ITask with id " + EntityId, "ITask", task);

                CheckRemoveTaskVariables(task);

                return task;
            }
        }

        protected internal override void LogVariableOperation(AbstractVariableScope scope)
        {
            var task = (TaskEntity)scope;
            CommandContext.OperationLogManager.LogVariableOperation(LogEntryOperation, null, task.Id,
                PropertyChange.EmptyChange);
        }

        protected internal virtual void CheckRemoveTaskVariables(TaskEntity task)
        {
            foreach (var checker in CommandContext.ProcessEngineConfiguration.CommandCheckers)
                checker.CheckUpdateTask(task);
        }
    }
}