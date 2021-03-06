﻿using ESS.FW.Bpm.Engine.Persistence.Entity;
using ESS.FW.Bpm.Engine.Runtime;

namespace ESS.FW.Bpm.Engine.Delegate
{
    /// <summary>
    ///     Execution used in <seealso cref="IJavaDelegate" />s and <seealso cref="IExecutionListener" />s.
    ///      
    /// </summary>
    public interface IDelegateExecution : IBaseDelegateExecution, IBpmnModelExecutionContext,
        IProcessEngineServicesAware
    {
        /// <summary>
        ///     Reference to the overall process instance
        /// </summary>
        string ProcessInstanceId { get; }

        /// <summary>
        ///     The business key for the process instance this execution is associated
        ///     with.
        /// </summary>
        string ProcessBusinessKey { get; }

        /// <summary>
        ///     The process definition key for the process instance this execution is
        ///     associated with.
        /// </summary>
        string ProcessDefinitionId { get; }

        /// <summary>
        ///     Gets the id of the parent of this execution. If null, the execution
        ///     represents a process-instance.
        /// </summary>
        string ParentId { get; }

        /// <summary>
        ///     Gets the id of the current activity.
        /// </summary>
        string CurrentActivityId { get; }

        /// <summary>
        ///     Gets the name of the current activity.
        /// </summary>
        string CurrentActivityName { get; }

        /// <summary>
        ///     return the Id of the activity instance currently executed by this execution
        /// </summary>
        string ActivityInstanceId { get; set; }

        /// <summary>
        ///     return the Id of the parent activity instance currently executed by this
        ///     execution
        /// </summary>
        string ParentActivityInstanceId { get; }

        /// <summary>
        ///     return the Id of the current transition
        /// </summary>
        string CurrentTransitionId { get; }

        /// <summary>
        ///     Return the process instance execution for this execution. In case this
        ///     execution is the process instance execution the method returns itself.
        /// </summary>
        IDelegateExecution ProcessInstance { get; }

        /// <summary>
        ///     In case this delegate execution is the process instance execution
        ///     and this process instance was started by a call activity, this method
        ///     returns the execution which executed the call activity in the super process instance.
        /// </summary>
        /// <returns> the super execution or null. </returns>
        IDelegateExecution SuperExecution { get; }

        /// <summary>
        ///     Returns whether this execution has been canceled.
        /// </summary>
        bool Canceled { get; }

        /// <summary>
        ///     Return the id of the tenant this execution belongs to. Can be <code>null</code>
        ///     if the execution belongs to no single tenant.
        /// </summary>
        string TenantId { get; }

        /// <summary>
        ///     MethodInfo to store variable in a specific scope identified by activity ID.
        /// </summary>
        /// <param name="variableName"> - name of the variable </param>
        /// <param name="value"> - value of the variable </param>
        /// <param name="activityId">
        ///     - activity ID which is associated with destination execution,
        ///     if not existing - exception will be thrown
        /// </param>
        /// <exception cref="ProcessEngineException"> if scope with specified activity ID is not found </exception>
        void SetVariable(string variableName, object value, string activityId);

        /// <summary>
        /// Create an incident associated with this execution
        /// </summary>
        /// <param name="incidentType"> the type of incident </param>
        /// <param name="configuration"> </param>
        /// <returns> a new incident </returns>
        IIncident CreateIncident(string incidentType, string configuration);

        /// <summary>
        /// Create an incident associated with this execution
        /// </summary>
        /// <param name="incidentType"> the type of incident </param>
        /// <param name="configuration"> </param>
        /// <param name="message"> </param>
        /// <returns> a new incident </returns>
        IIncident CreateIncident(string incidentType, string configuration, string message);

        /// <summary>
        /// Resolve and remove an incident with given id
        /// </summary>
        /// <param name="incidentId"> </param>
        void ResolveIncident(string incidentId);

    }
}