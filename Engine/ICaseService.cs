﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ESS.FW.Bpm.Engine.exception;
using ESS.FW.Bpm.Engine.Runtime;
using ESS.FW.Bpm.Engine.Task;
using ESS.FW.Bpm.Engine.Variable;

namespace ESS.FW.Bpm.Engine
{
    /// <summary>
    ///     Service which provides access to <seealso cref="ICaseInstance" />
    ///     and <seealso cref="ICaseExecution" />.
    ///     
    /// </summary>
    public interface ICaseService
    {
        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> of the latest version of the case definition
        ///         with the given key. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionKey">
        ///     the key of the case definition to instantiate
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition key is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given key. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceByKey(string caseDefinitionKey);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> of the latest version of the case definition
        ///         with the given key. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        ///     <para>
        ///         A business key can be provided to associate the case instance with a
        ///         certain identifier that has a clear business meaning. This business key can
        ///         then be used to easily look up that case instance, see
        ///         <seealso cref="IQueryable<ICaseInstance>#caseInstanceBusinessKey(String)" />. Providing such a
        ///         business key is definitely a best practice.
        ///     </para>
        ///     <para>
        ///         Note that a business key MUST be unique for the given case definition WHEN
        ///         you have added a database constraint for it. In this case, only case instance
        ///         from different case definition are allowed to have the same business key and
        ///         the combination of caseDefinitionKey-businessKey must be unique.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionKey"> the key of the case definition to instantiate </param>
        /// <param name="businessKey">
        ///     a key that uniquely identifies the case instance in the context
        ///     of the given case definition.
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition key is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given key. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceByKey(string caseDefinitionKey, string businessKey);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> of the latest version of the case definition
        ///         with the given key. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionKey"> the key of the case definition to instantiate </param>
        /// <param name="variables">
        ///     variables to be set on the new case instance
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition key is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given key. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceByKey(string caseDefinitionKey, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> of the latest version of the case definition
        ///         with the given key. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        ///     <para>
        ///         A business key can be provided to associate the case instance with a
        ///         certain identifier that has a clear business meaning. This business key can
        ///         then be used to easily look up that case instance, see
        ///         <seealso cref="IQueryable<ICaseInstance>#caseInstanceBusinessKey(String)" />. Providing such a
        ///         business key is definitely a best practice.
        ///     </para>
        ///     <para>
        ///         Note that a business key MUST be unique for the given case definition WHEN
        ///         you have added a database constraint for it. In this case, only case instance
        ///         from different case definition are allowed to have the same business key and
        ///         the combination of caseDefinitionKey-businessKey must be unique.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionKey"> the key of the case definition to instantiate. </param>
        /// <param name="businessKey">
        ///     a key that uniquely identifies the case instance in the context
        ///     of the given case definition.
        /// </param>
        /// <param name="variables">
        ///     variables to be set on the new case instance.
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition key is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given key. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceByKey(string caseDefinitionKey, string businessKey,
            IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> in the exactly specified version identify by the provided
        ///         process definition id. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionId">
        ///     the id of the case definition to instantiate
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition id is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given id. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceById(string caseDefinitionId);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> in the exactly specified version identify by the provided
        ///         process definition id. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        ///     <para>
        ///         A business key can be provided to associate the case instance with a
        ///         certain identifier that has a clear business meaning. This business key can
        ///         then be used to easily look up that case instance, see
        ///         <seealso cref="IQueryable<ICaseInstance>#caseInstanceBusinessKey(String)" />. Providing such a
        ///         business key is definitely a best practice.
        ///     </para>
        ///     <para>
        ///         Note that a business key MUST be unique for the given case definition WHEN
        ///         you have added a database constraint for it. In this case, only case instance
        ///         from different case definition are allowed to have the same business key and
        ///         the combination of caseDefinitionKey-businessKey must be unique.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionId"> the id of the case definition to instantiate </param>
        /// <param name="businessKey">
        ///     a key that uniquely identifies the case instance in the context
        ///     of the given case definition.
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition id is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given id. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceById(string caseDefinitionId, string businessKey);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> in the exactly specified version identify by the provided
        ///         process definition id. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionId"> the id of the case definition to instantiate </param>
        /// <param name="variables">
        ///     variables to be set on the new case instance.
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition id is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given id. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceById(string caseDefinitionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="ICaseInstance" /> in the exactly specified version identify by the provided
        ///         process definition id. The new case instance will be in the <code>ACTIVE</code> state.
        ///     </para>
        ///     <para>
        ///         A business key can be provided to associate the case instance with a
        ///         certain identifier that has a clear business meaning. This business key can
        ///         then be used to easily look up that case instance, see
        ///         <seealso cref="IQueryable<ICaseInstance>#caseInstanceBusinessKey(String)" />. Providing such a
        ///         business key is definitely a best practice.
        ///     </para>
        ///     <para>
        ///         Note that a business key MUST be unique for the given case definition WHEN
        ///         you have added a database constraint for it. In this case, only case instance
        ///         from different case definition are allowed to have the same business key and
        ///         the combination of caseDefinitionKey-businessKey must be unique.
        ///     </para>
        /// </summary>
        /// <param name="caseDefinitionId"> the id of the case definition to instantiate </param>
        /// <param name="businessKey">
        ///     a key that uniquely identifies the case instance in the context
        ///     of the given case definition.
        /// </param>
        /// <param name="variables">
        ///     variables to be set on the new case instance.
        /// </param>
        /// <exception cref="NotValidException"> when the given case definition id is null. </exception>
        /// <exception cref="NotFoundException"> when no case definition is deployed with the given id. </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        ICaseInstance CreateCaseInstanceById(string caseDefinitionId, string businessKey,
            IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Starts the case execution identified by the given id manually.
        ///         Performs the transition from state
        ///         <code>ENABLED</code> to state <code>ACTIVE</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>ACTIVE</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution does the following:
        ///         <ul>
        ///             <li><seealso cref="ITask" />: the <seealso cref="ITask" /> is completed immediately</li>
        ///             <li>
        ///                 <seealso cref="HumanTask" />: a new <seealso cref="ITask" /> is
        ///                 instantiated
        ///             </li>
        ///             <li>
        ///                 <seealso cref="ProcessTask" />: a new <seealso cref="ProcessInstance process instance" /> is
        ///                 instantiated
        ///             </li>
        ///             <li><seealso cref="CaseTask" />: a new <seealso cref="ICaseInstance" /> is instantiated</li>
        ///         </ul>
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to manually start
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void ManuallyStartCaseExecution(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Starts the case execution identified by the given id manually.
        ///         Performs a transition from state
        ///         <code>ENABLED</code> to state <code>ACTIVE</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>ACTIVE</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution does the following:
        ///         <ul>
        ///             <li><seealso cref="ITask" />: the <seealso cref="ITask" /> is completed immediately</li>
        ///             <li>
        ///                 <seealso cref="HumanTask" />: a new <seealso cref="ITask" /> is
        ///                 instantiated
        ///             </li>
        ///             <li>
        ///                 <seealso cref="ProcessTask" />: a new <seealso cref="ProcessInstance process instance" /> is
        ///                 instantiated
        ///             </li>
        ///             <li><seealso cref="CaseTask" />: a new <seealso cref="ICaseInstance" /> is instantiated</li>
        ///         </ul>
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of the case execution to manually start </param>
        /// <param name="variables">
        ///     variables to be set on the case execution
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void ManuallyStartCaseExecution(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Disables the case execution  identified by the given id.
        ///         Performs a transition from state <code>ENABLED</code>
        ///         to state <code>DISABLED</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>DISABLED</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution should not be executed
        ///         in this case instance.
        ///     </para>
        ///     <para>
        ///         If the given case execution has a parent case execution, that parent
        ///         case execution will be notified that the given case execution has been
        ///         disabled. This can lead to a completion of the parent case execution if
        ///         the completion criteria are fulfilled.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to disable
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void DisableCaseExecution(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Disables the case execution identified by the given id.
        ///         Performs a transition from state <code>ENABLED</code>
        ///         to state <code>DISABLED</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>DISABLED</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution should not be executed
        ///         in this case instance.
        ///     </para>
        ///     <para>
        ///         If the given case execution has a parent case execution, that parent
        ///         case execution will be notified that the given case execution has been
        ///         disabled. This can lead to a completion of the parent case execution if
        ///         the completion criteria are fulfilled.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of the case execution to disable </param>
        /// <param name="variables">
        ///     variables to be set on the case execution
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void DisableCaseExecution(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Re-enables the case execution identified by the given id.
        ///         Performs a transition from state <code>DISABLED</code>
        ///         to state <code>ENABLED</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>DISABLED</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution pends for a decision
        ///         to become <code>ACTIVE</code> or <code>DISABLED</code>.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to re-enable
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void ReenableCaseExecution(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Re-enables the case execution identified by the given id.
        ///         Performs a transition from state <code>DISABLED</code>
        ///         to state <code>ENABLED</code>.
        ///     </para>
        ///     <para>
        ///         According to CMMN 1.0 specification, the state <code>DISABLED</code> means that the
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" /> related to the case execution pends for a decision
        ///         to become <code>ACTIVE</code> or <code>DISABLED</code>.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of the case execution to re-enable </param>
        /// <param name="variables">
        ///     variables to be set on the case execution
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException">
        ///     when the transition is not allowed to be done or
        ///     when the case execution is a case instance
        /// </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void ReenableCaseExecution(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Completes the case execution identified by the given id.
        ///         Performs a transition from state <code>ACTIVE</code>
        ///         to state <code>COMPLETED</code>.
        ///     </para>
        ///     <para>
        ///         It is only possible to complete a case execution which is associated with a
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" />.
        ///     </para>
        ///     <para>
        ///         In case of a <seealso cref="Stage" />, the completion can only be performed when the following
        ///         criteria are fulfilled:
        ///         <br>
        ///             <ul>
        ///                 <li>there are no children in the state <code>ACTIVE</code></li>
        ///             </ul>
        ///     </para>
        ///     <para>
        ///         For a <seealso cref="ITask" /> instance, this means its purpose has been accomplished:
        ///         <br>
        ///             <ul>
        ///                 <li><seealso cref="HumanTask" /> has been completed by human.</li>
        ///             </ul>
        ///     </para>
        ///     <para>
        ///         If the given case execution has a parent case execution, that parent
        ///         case execution will be notified that the given case execution has been
        ///         completed. This can lead to a completion of the parent case execution if
        ///         the completion criteria are fulfilled.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to complete
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException"> when the transition is not allowed to be done </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void CompleteCaseExecution(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Completes the case execution identified by the given id.
        ///         Performs a transition from state <code>ACTIVE</code>
        ///         to state <code>COMPLETED</code>.
        ///     </para>
        ///     <para>
        ///         It is only possible to complete a case execution which is associated with a
        ///         <seealso cref="Stage" /> or <seealso cref="ITask" />.
        ///     </para>
        ///     <para>
        ///         In case of a <seealso cref="Stage" />, the completion can only be performed when the following
        ///         criteria are fulfilled:
        ///         <br>
        ///             <ul>
        ///                 <li>there are no children in the state <code>ACTIVE</code></li>
        ///             </ul>
        ///     </para>
        ///     <para>
        ///         For a <seealso cref="ITask" /> instance, this means its purpose has been accomplished:
        ///         <br>
        ///             <ul>
        ///                 <li><seealso cref="HumanTask" /> has been completed by human.</li>
        ///             </ul>
        ///     </para>
        ///     <para>
        ///         If the given case execution has a parent case execution, that parent
        ///         case execution will be notified that the given case execution has been
        ///         completed. This can lead to a completion of the parent case execution if
        ///         the completion criteria are fulfilled.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of the case execution to complete </param>
        /// <param name="variables">
        ///     variables to be set on the case execution
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException"> when the transition is not allowed to be done </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void CompleteCaseExecution(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Closes the case instance the execution identified by the given id
        ///         belongs to. Once closed, no further work or modifications are
        ///         allowed for the case instance.
        ///         Performs a transition from state <code>COMPLETED</code>
        ///         to state <code>CLOSED</code>.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to close
        ///     the case instance for
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException"> when the transition is not allowed to be done </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void CloseCaseInstance(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Terminates the case execution identified by the given id.
        ///         Performs the transition from <code>ACTIVE</code> to state <code>TERMINATED</code>
        ///         if the case execution belongs to a case model or a task or a stage.
        ///         Performs the transition from <code>AVAILABLE</code> to state <code>TERMINATED</code> if the case
        ///         execution belongs to a milestone.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of the case execution to be terminated
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException"> when the transition is not allowed to be done </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void TerminateCaseExecution(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         Terminates the case execution identified by the given id.
        ///         Performs the transition from <code>ACTIVE</code> to state <code>TERMINATED</code>
        ///         if the case execution belongs to either a case model or a task or a stage.
        ///         Performs the transition from <code>AVAILABLE</code> to state <code>TERMINATED</code> if the case
        ///         execution belongs to a milestone.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of the case execution to terminate </param>
        /// <param name="variables">
        ///     variables to be set on the case execution
        /// </param>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException">
        ///     when no case execution is found for the
        ///     given case execution id
        /// </exception>
        /// <exception cref="NotAllowedException"> when the transition is not allowed to be done </exception>
        /// <exception cref="ProcessEngineException">
        ///     when an internal exception happens during the execution
        ///     of the command.
        /// </exception>
        void TerminateCaseExecution(string caseExecutionId, IDictionary<string, object> variables);

        // fluent API ////////////////////////////////////////////////////////////

        /// <summary>
        ///     <para>Define a <seealso cref="ICaseInstance" /> using a fluent builder.</para>
        ///     <para>Starts a new case instance with the latest version of the corresponding case definition.</para>
        /// </summary>
        /// <param name="caseDefinitionKey">
        ///     the key of a case definition to create a new case instance of, cannot be null
        /// </param>
        /// <returns> a <seealso cref="CaseInstanceBuilder fluent builder" /> for defining a new case instance </returns>
        ICaseInstanceBuilder WithCaseDefinitionByKey(string caseDefinitionKey);

        /// <summary>
        ///     <para>Define a <seealso cref="ICaseInstance" /> using a fluent builder.</para>
        ///     <para>Starts a new case instance with the case definition version corresponding to the given id.</para>
        /// </summary>
        /// <param name="caseDefinitionId">
        ///     the id of a case definition to create a new case instance, cannot be null
        /// </param>
        /// <returns> a <seealso cref="CaseInstanceBuilder fluent builder" /> for defining a new case instance </returns>
        ICaseInstanceBuilder WithCaseDefinition(string caseDefinitionId);

        /// <summary>
        ///     <para>Define a command to be executed for a <seealso cref="ICaseExecution" /> using a fluent builder.</para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of a case execution to define a command for it
        /// </param>
        /// <returns>
        ///     a <seealso cref="CaseExecutionCommandBuilder fluent builder" /> for defining a command
        ///     for a case execution
        /// </returns>
        ICaseExecutionCommandBuilder WithCaseExecution(string caseExecutionId);

        // Query API ///////////////////////////////////////////////////////////

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="IQueryable<ICaseInstance>" /> instance, that can be used
        ///         to query case instances.
        ///     </para>
        /// </summary>
        IQueryable<ICaseInstance> CreateCaseInstanceQuery();

        /// <summary>
        ///     <para>
        ///         Creates a new <seealso cref="IQueryable<ICaseExecution>" /> instance,
        ///         that can be used to query the executions and case instances.
        ///     </para>
        /// </summary>
        IQueryable<ICaseExecution> CreateCaseExecutionQuery(Expression<Func<ICaseExecution,bool>> expression =null );

        // Variables //////////////////////////////////////////////////////////

        /// <summary>
        ///     <para>All variables visible from the given execution scope (including parent scopes).</para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariables(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of a case instance or case execution, cannot be null
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IDictionary<string, object> GetVariables(string caseExecutionId);

        /// <summary>
        ///     <para>All variables visible from the given execution scope (including parent scopes).</para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariables(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of a case instance or case execution, cannot be null
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesTyped(string caseExecutionId);

        /// <summary>
        ///     <para>All variables visible from the given execution scope (including parent scopes).</para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariables(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="deserializeValues">
        ///     if false, the process engine will not attempt to deserialize
        ///     <seealso cref="SerializableValue SerializableValues" />.
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesTyped(string caseExecutionId, bool deserializeValues);

        /// <summary>
        ///     <para>
        ///         All variable values that are defined in the case execution scope, without
        ///         taking outer scopes into account.
        ///     </para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariablesLocal(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of a case execution, cannot be null
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IDictionary<string, object> GetVariablesLocal(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         All variable values that are defined in the case execution scope, without
        ///         taking outer scopes into account.
        ///     </para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariablesLocal(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId">
        ///     the id of a case execution, cannot be null
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesLocalTyped(string caseExecutionId);

        /// <summary>
        ///     <para>
        ///         All variable values that are defined in the case execution scope, without
        ///         taking outer scopes into account.
        ///     </para>
        ///     <para>
        ///         If you have many local variables and you only need a few, consider
        ///         using <seealso cref="#getVariablesLocal(String, Collection)" /> for better performance.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case execution, cannot be null </param>
        /// <param name="deserializeValues">
        ///     if false, the process engine will not attempt to deserialize
        ///     <seealso cref="SerializableValue SerializableValues" />.
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesLocalTyped(string caseExecutionId, bool deserializeValues);

        /// <summary>
        ///     <para>
        ///         The variable values for all given variableNames, takes all variables
        ///         into account which are visible from the given case execution scope
        ///         (including parent scopes).
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableNames">
        ///     the collection of variable names that should be retrieved
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IDictionary<string, object> GetVariables(string caseExecutionId, ICollection<string> variableNames);

        /// <summary>
        ///     <para>
        ///         The variable values for all given variableNames, takes all variables
        ///         into account which are visible from the given case execution scope
        ///         (including parent scopes).
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableNames"> the collection of variable names that should be retrieved </param>
        /// <param name="deserializeValues">
        ///     if false, <seealso cref="SerializableValue SerializableValues" /> will not be deserialized
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesTyped(string caseExecutionId, ICollection<string> variableNames, bool deserializeValues);

        /// <summary>
        ///     <para>
        ///         The variable values for the given variableNames only taking the given case
        ///         execution scope into account, not looking in outer scopes.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case execution, cannot be null </param>
        /// <param name="variableNames">
        ///     the collection of variable names that should be retrieved
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IDictionary<string, object> GetVariablesLocal(string caseExecutionId, ICollection<string> variableNames);

        /// <summary>
        ///     <para>
        ///         The variable values for the given variableNames only taking the given case
        ///         execution scope into account, not looking in outer scopes.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case execution, cannot be null </param>
        /// <param name="variableNames"> the collection of variable names that should be retrieved </param>
        /// <param name="deserializeValues">
        ///     if false, the process engine will not attempt to deserialize
        ///     <seealso cref="SerializableValue SerializableValues" />.
        /// </param>
        /// <returns>
        ///     the variables or an empty map if no such variables are found
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        IVariableMap GetVariablesLocalTyped(string caseExecutionId, ICollection<string> variableNames,
            bool deserializeValues);

        /// <summary>
        ///     <para>
        ///         Searching for the variable is done in all scopes that are visible
        ///         to the given case execution (including parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or
        ///         when the value is set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName">
        ///     the name of a variable, cannot be null
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        object GetVariable(string caseExecutionId, string variableName);

        /// <summary>
        ///     <para>
        ///         Searching for the variable is done in all scopes that are visible
        ///         to the given case execution (including parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or
        ///         when the value is set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName">
        ///     the name of a variable, cannot be null
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
        T GetVariableTyped<T>(string caseExecutionId, string variableName);

        /// <summary>
        ///     <para>
        ///         Searching for the variable is done in all scopes that are visible
        ///         to the given case execution (including parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or
        ///         when the value is set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName"> the name of a variable, cannot be null </param>
        /// <param name="deserializeValue">
        ///     if false, <seealso cref="SerializableValue SerializableValues" /> will not be deserialized
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
        T GetVariableTyped<T>(string caseExecutionId, string variableName, bool deserializeValue);

        /// <summary>
        ///     <para>
        ///         The variable value for an case execution. Returns the value when the variable is set
        ///         for the case execution (and not searching parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or when the value is
        ///         set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName">
        ///     the name of a variable, cannot be null
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
        object GetVariableLocal(string caseExecutionId, string variableName);

        /// <summary>
        ///     <para>
        ///         The variable value for an case execution. Returns the value when the variable is set
        ///         for the case execution (and not searching parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or when the value is
        ///         set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName">
        ///     the name of a variable, cannot be null
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
        T GetVariableLocalTyped<T>(string caseExecutionId, string variableName);

        /// <summary>
        ///     <para>
        ///         The variable value for an case execution. Returns the value when the variable is set
        ///         for the case execution (and not searching parent scopes).
        ///     </para>
        ///     <para>
        ///         Returns null when no variable value is found with the given name or when the value is
        ///         set to null.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the id of a case instance or case execution, cannot be null </param>
        /// <param name="variableName"> the name of a variable, cannot be null </param>
        /// <param name="deserializeValue">
        ///     if false, <seealso cref="SerializableValue SerializableValues" /> will not be deserialized
        /// </param>
        /// <returns>
        ///     the variable value or null if the variable is undefined or the value of the variable is null
        /// </returns>
        /// <exception cref="NotValidException"> when the given case execution id or variable name is null </exception>
        /// <exception cref="NotFoundException"> when no case execution is found for the given case execution id </exception>
        /// <exception cref="ProcessEngineException"> when an internal exception happens during the execution of the command </exception>
//JAVA TO C# CONVERTER TODO TASK: The following line could not be converted:
        T GetVariableLocalTyped<T>(string caseExecutionId, string variableName, bool deserializeValue);

        /// <summary>
        ///     <para>
        ///         Pass a map of variables to the case execution. If the variables do not already
        ///         exist, they are created in the case instance (which is the root execution).
        ///         Otherwise existing variables are updated.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to set the variables for </param>
        /// <param name="variables"> the map of variables </param>
        void SetVariables(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>Pass a map of variables to the case execution (not considering parent scopes).</para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to set the variables for </param>
        /// <param name="variables"> the map of variables </param>
        void SetVariablesLocal(string caseExecutionId, IDictionary<string, object> variables);

        /// <summary>
        ///     <para>
        ///         Pass a variable to the case execution. If the variable does not already
        ///         exist, it is created in the case instance (which is the root execution).
        ///         Otherwise, the existing variable is updated.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to set the variable for </param>
        /// <param name="variableName"> the name of the variable to set </param>
        /// <param name="variableValue">
        ///     the value of the variable to set
        /// </param>
        /// <exception cref="NotValidException"> when the given variable name is null </exception>
        void SetVariable(string caseExecutionId, string variableName, object variableValue);

        /// <summary>
        ///     <para>Pass a local variable to the case execution (not considering parent scopes).</para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to set the variable for </param>
        /// <param name="variableName"> the name of the variable to set </param>
        /// <param name="variableValue">
        ///     the value of the variable to set
        /// </param>
        /// <exception cref="NotValidException"> when the given variable name is null </exception>
        void SetVariableLocal(string caseExecutionId, string variableName, object variableValue);

        /// <summary>
        ///     <para>
        ///         Pass a collection of names identifying variables to be removed from a
        ///         case execution.
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to remove the variables from </param>
        /// <param name="variableNames"> a collection of names of variables to remove </param>
        void RemoveVariables(string caseExecutionId, ICollection<string> variableNames);

        /// <summary>
        ///     <para>
        ///         Pass a collection of names identifying local variables to be removed from a
        ///         case execution (not considering parent scopes).
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to remove the variables from </param>
        /// <param name="variableNames"> a collection of names of variables to remove </param>
        void RemoveVariablesLocal(string caseExecutionId, ICollection<string> variableNames);

        /// <summary>
        ///     <para>Pass a name of a variable to be removed from a case execution.</para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to remove the variable from </param>
        /// <param name="variableName">
        ///     the name of the variable to remove
        /// </param>
        /// <exception cref="NotValidException"> when the given variable name is null </exception>
        void RemoveVariable(string caseExecutionId, string variableName);

        /// <summary>
        ///     <para>
        ///         Pass a variable name of a local variable to be removed from a case execution
        ///         (not considering parent scopes).
        ///     </para>
        /// </summary>
        /// <param name="caseExecutionId"> the case execution to remove the variable from </param>
        /// <param name="variableName">
        ///     the name of a variable to remove
        /// </param>
        /// <exception cref="NotValidException"> when the given variable name is null </exception>
        void RemoveVariableLocal(string caseExecutionId, string variableName);
    }
}