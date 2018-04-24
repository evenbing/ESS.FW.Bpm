﻿using System;
using ESS.FW.Bpm.Engine.Impl.Interceptor;
using ESS.FW.Bpm.Engine.Impl.Util;

namespace ESS.FW.Bpm.Engine.Impl.Cmd
{

    /// <summary>
    /// </summary>
    [Serializable]
    public class CreateMembershipCmd : AbstractWritableIdentityServiceCmd<object>, ICommand<object>
    {
        private const long SerialVersionUid = 1L;
        internal string GroupId;

        internal string UserId;

        public CreateMembershipCmd(string userId, string groupId)
        {
            this.UserId = userId;
            this.GroupId = groupId;
        }

        protected internal override object ExecuteCmd(CommandContext commandContext)
        {
            EnsureUtil.EnsureNotNull("userId", UserId);
            EnsureUtil.EnsureNotNull("groupId", GroupId);
            commandContext.WritableIdentityProvider.CreateMembership(UserId, GroupId);
            return null;
        }
    }
}