﻿using System.Collections.Generic;
using System.Linq;
using ESS.FW.Bpm.Engine;
using ESS.FW.Bpm.Engine.Authorization;
using ESS.FW.Bpm.Engine.History;
using ESS.FW.Bpm.Engine.Task;
using NUnit.Framework;

namespace Engine.Tests.Api.Authorization.History
{

    [RequiredHistoryLevel(ProcessEngineConfiguration.HistoryFull)]
    public class HistoricVariableInstanceAuthorizationTest : AuthorizationTest
    {

        protected internal const string PROCESS_KEY = "oneTaskProcess";
        protected internal const string MESSAGE_START_PROCESS_KEY = "messageStartProcess";
        protected internal const string CASE_KEY = "oneTaskCase";

        protected internal string deploymentId;

        [SetUp]
        public void setUp()
        {
            deploymentId = createDeployment(null, "resources/api/oneTaskProcess.bpmn20.xml", "resources/api/authorization/messageStartEventProcess.bpmn20.xml", "resources/api/authorization/oneTaskCase.cmmn").Id;
            base.setUp();
        }

        public void tearDown()
        {
            base.TearDown();
            DeleteDeployment(deploymentId);
        }

        // historic variable instance query (standalone task) /////////////////////////////////////////////

        public virtual void testQueryAfterStandaloneTaskVariables()
        {
            // given
            string taskId = "myTask";
            createTask(taskId);

            disableAuthorization();
            taskService.SetVariables(taskId, Variables);
            enableAuthorization();

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 1);

            deleteTask(taskId, true);
        }

        // historic variable instance query (process variables) ///////////////////////////////////////////

        public virtual void testSimpleQueryWithoutAuthorization()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 0);
        }

        public virtual void testSimpleQueryWithReadHistoryPermissionOnProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            createGrantAuthorization(Resources.ProcessDefinition, PROCESS_KEY, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 1);
        }

        public virtual void testSimpleQueryWithReadHistoryPermissionOnAnyProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            createGrantAuthorization(Resources.ProcessDefinition, AuthorizationFields.Any, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 1);
        }

        public virtual void testSimpleQueryWithMultiple()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            createGrantAuthorization(Resources.ProcessDefinition, AuthorizationFields.Any, userId, Permissions.ReadHistory);
            createGrantAuthorization(Resources.ProcessDefinition, PROCESS_KEY, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 1);
        }

        // historic variable instance query (multiple process instances) ////////////////////////

        public virtual void testQueryWithoutAuthorization()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 0);
        }

        public virtual void testQueryWithReadHistoryPermissionOnProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            createGrantAuthorization(Resources.ProcessDefinition, PROCESS_KEY, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 3);
        }

        public virtual void testQueryWithReadHistoryPermissionOnAnyProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            createGrantAuthorization(Resources.ProcessDefinition, AuthorizationFields.Any, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 7);
        }

        // historic variable instance query (case variables) /////////////////////////////////////////////

        public virtual void testQueryAfterCaseVariables()
        {
            // given
            createCaseInstanceByKey(CASE_KEY, Variables);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 1);
        }

        // historic variable instance query (mixed variables) ////////////////////////////////////

        public virtual void testMixedQueryWithoutAuthorization()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            createTask("one");
            createTask("two");
            createTask("three");
            createTask("four");
            createTask("five");

            disableAuthorization();
            taskService.SetVariables("one", Variables);
            taskService.SetVariables("two", Variables);
            taskService.SetVariables("three", Variables);
            taskService.SetVariables("four", Variables);
            taskService.SetVariables("five", Variables);
            enableAuthorization();

            createCaseInstanceByKey(CASE_KEY, Variables);
            createCaseInstanceByKey(CASE_KEY, Variables);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 7);

            deleteTask("one", true);
            deleteTask("two", true);
            deleteTask("three", true);
            deleteTask("four", true);
            deleteTask("five", true);
        }

        public virtual void testMixedQueryWithReadHistoryPermissionOnProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            createTask("one");
            createTask("two");
            createTask("three");
            createTask("four");
            createTask("five");

            disableAuthorization();
            taskService.SetVariables("one", Variables);
            taskService.SetVariables("two", Variables);
            taskService.SetVariables("three", Variables);
            taskService.SetVariables("four", Variables);
            taskService.SetVariables("five", Variables);
            enableAuthorization();

            createCaseInstanceByKey(CASE_KEY, Variables);
            createCaseInstanceByKey(CASE_KEY, Variables);

            createGrantAuthorization(Resources.ProcessDefinition, PROCESS_KEY, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 10);

            deleteTask("one", true);
            deleteTask("two", true);
            deleteTask("three", true);
            deleteTask("four", true);
            deleteTask("five", true);
        }

        public virtual void testMixedQueryWithReadHistoryPermissionOnAnyProcessDefinition()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);

            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);
            StartProcessInstanceByKey(MESSAGE_START_PROCESS_KEY, Variables);

            createTask("one");
            createTask("two");
            createTask("three");
            createTask("four");
            createTask("five");

            disableAuthorization();
            taskService.SetVariables("one", Variables);
            taskService.SetVariables("two", Variables);
            taskService.SetVariables("three", Variables);
            taskService.SetVariables("four", Variables);
            taskService.SetVariables("five", Variables);
            enableAuthorization();

            createCaseInstanceByKey(CASE_KEY, Variables);
            createCaseInstanceByKey(CASE_KEY, Variables);

            createGrantAuthorization(Resources.ProcessDefinition, AuthorizationFields.Any, userId, Permissions.ReadHistory);

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 14);

            deleteTask("one", true);
            deleteTask("two", true);
            deleteTask("three", true);
            deleteTask("four", true);
            deleteTask("five", true);
        }

        // Delete deployment (cascade = false)

        public virtual void testQueryAfterDeletingDeployment()
        {
            // given
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            StartProcessInstanceByKey(PROCESS_KEY, Variables);
            createGrantAuthorization(Resources.ProcessDefinition, PROCESS_KEY, userId, Permissions.ReadHistory);

            disableAuthorization();
            IList<ITask> tasks = taskService.CreateTaskQuery().ToList();
            foreach (ITask task in tasks)
            {
                taskService.Complete(task.Id);
            }
            enableAuthorization();

            disableAuthorization();
            repositoryService.DeleteDeployment(deploymentId);
            enableAuthorization();

            // when
            IQueryable<IHistoricVariableInstance> query = historyService.CreateHistoricVariableInstanceQuery();

            // then
            //verifyQueryResults(query, 3);

            disableAuthorization();
            IList<IHistoricProcessInstance> instances = historyService.CreateHistoricProcessInstanceQuery()
                .ToList();
            foreach (IHistoricProcessInstance instance in instances)
            {
                historyService.DeleteHistoricProcessInstance(instance.Id);
            }
            enableAuthorization();
        }

        // helper ////////////////////////////////////////////////////////

        protected internal virtual void verifyQueryResults(IQueryable<IHistoricVariableInstance> query, int countExpected)
        {
            //JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
            //ORIGINAL LINE: //verifyQueryResults((org.camunda.bpm.Engine.impl.AbstractQuery<?, ?>) query, countExpected);
            ////verifyQueryResults((AbstractQuery<object, object>)query, countExpected);
        }

    }

}