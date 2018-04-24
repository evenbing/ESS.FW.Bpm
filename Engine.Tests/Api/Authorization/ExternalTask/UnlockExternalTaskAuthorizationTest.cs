﻿using System.Collections.Generic;
using System.Linq;
using Engine.Tests.Api.Authorization.Util;
using Engine.Tests.Util;
using ESS.FW.Bpm.Engine.Authorization;
using ESS.FW.Bpm.Engine.Externaltask;
using ESS.FW.Bpm.Engine.Runtime;
using NUnit.Framework;

namespace Engine.Tests.Api.Authorization.ExternalTask
{



    /// <summary>
    /// 
    /// 
    /// </summary>
    //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
    //ORIGINAL LINE: @RunWith(Parameterized.class) public class UnlockExternalTaskAuthorizationTest
    public class UnlockExternalTaskAuthorizationTest
    {
        private bool InstanceFieldsInitialized = false;

        public UnlockExternalTaskAuthorizationTest()
        {
            if (!InstanceFieldsInitialized)
            {
                InitializeInstanceFields();
                InstanceFieldsInitialized = true;
            }
        }

        private void InitializeInstanceFields()
        {
            authRule = new AuthorizationTestRule(engineRule);
            //chain = RuleChain.outerRule(engineRule).around(authRule);
        }


        public ProcessEngineRule engineRule = new ProvidedProcessEngineRule();
        public AuthorizationTestRule authRule;

        //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Rule public org.junit.Rules.RuleChain chain = org.junit.Rules.RuleChain.outerRule(engineRule).around(authRule);
        //public RuleChain chain;

        //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Parameter public api.authorization.util.AuthorizationScenario scenario;
        public AuthorizationScenario scenario;

        //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Parameters(name = "Scenario {index}") public static java.util.Collection<api.authorization.util.AuthorizationScenario[]> scenarios()
        public static ICollection<AuthorizationScenario[]> scenarios()
        {
            return AuthorizationTestRule.AsParameters(AuthorizationScenario.Scenario().WithoutAuthorizations()
                .FailsDueToRequired(AuthorizationSpec.Grant(Resources.ProcessInstance, "ProcessInstanceId", "userId", Permissions.Update), 
                AuthorizationSpec.Grant(Resources.ProcessDefinition, "oneExternalTaskProcess", "userId", Permissions.UpdateInstance)), 
                AuthorizationScenario.Scenario().WithAuthorizations(AuthorizationSpec.Grant(Resources.ProcessInstance, "ProcessInstanceId", "userId", Permissions.Update))
                .Succeeds(), AuthorizationScenario.Scenario().WithAuthorizations(AuthorizationSpec.Grant(Resources.ProcessInstance, "*", "userId", Permissions.Update))
                .Succeeds(), AuthorizationScenario.Scenario().WithAuthorizations(AuthorizationSpec.Grant(Resources.ProcessDefinition, "processDefinitionKey", "userId", 
                Permissions.UpdateInstance)).Succeeds(), AuthorizationScenario.Scenario()
                .WithAuthorizations(AuthorizationSpec.Grant(Resources.ProcessDefinition, "*", "userId", Permissions.UpdateInstance)).Succeeds());
        }

        //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @Before public void setUp()
        public virtual void setUp()
        {
            authRule.CreateUserAndGroup("userId", "groupId");
        }

        //JAVA TO C# CONVERTER TODO Resources.Task: Most Java annotations will not have direct .NET equivalent attributes:
        //ORIGINAL LINE: @After public void tearDown()
        public virtual void tearDown()
        {
            authRule.DeleteUsersAndGroups();
        }

        [Test]
        [Deployment(new string[] { "resources/api/externaltask/oneExternalTaskProcess.bpmn20.xml" })]
        public virtual void testSetJobPriority()
        {

            // given
            IProcessInstance processInstance = engineRule.RuntimeService.StartProcessInstanceByKey("oneExternalTaskProcess");
            IList<ILockedExternalTask> tasks = engineRule.ExternalTaskService.FetchAndLock(5, "workerId")
                //.Topic("externalTaskTopic", 5000L).Execute(); 
                as IList<ILockedExternalTask>;
            ILockedExternalTask task = tasks[0];

            // when
            authRule.Init(scenario).WithUser("userId").BindResource("ProcessInstanceId", processInstance.Id).BindResource("processDefinitionKey", "oneExternalTaskProcess").Start();

            engineRule.ExternalTaskService.Unlock(task.Id);

            // then
            if (authRule.AssertScenario(scenario))
            {
                ESS.FW.Bpm.Engine.Externaltask.IExternalTask externalTask = engineRule.ExternalTaskService.CreateExternalTaskQuery().First();
                Assert.IsNull(externalTask.LockExpirationTime);
            }

        }


    }

}