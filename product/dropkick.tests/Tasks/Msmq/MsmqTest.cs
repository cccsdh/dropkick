// Copyright 2007-2010 The Apache Software Foundation.
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace dropkick.tests.Tasks.Msmq
{
    using System;
    using System.Messaging;
    using dropkick.DeploymentModel;
    using dropkick.Tasks.Msmq;
    using Dsl.Msmq;
    using NUnit.Framework;

    [TestFixture]
    [Category("Integration")]
    public class MsmqTest
    {
        [Test]
        public void ExecuteLocal()
        {
            var ps = new DeploymentServer(Environment.MachineName);
            var ub = new UriBuilder("msmq", ps.Name) { Path = "dk_test" };
            var address = new QueueAddress(ub.Uri);

            if (MessageQueue.Exists(address.LocalName))
                MessageQueue.Delete(address.LocalName);

            var t = new MsmqTask(ps, address);
            var r = t.Execute();

            Assert.IsFalse(r.ContainsError(), "Errors occured during MSMQ create execution.");

        }

        [Test]
        public void VerifyLocal()
        {
            var ps = new DeploymentServer(Environment.MachineName);
            var ub = new UriBuilder("msmq", ps.Name) { Path = "dk_test" };
            var t = new MsmqTask(ps, new QueueAddress(ub.Uri));
            var r = t.VerifyCanRun();

            Assert.IsFalse(r.ContainsError(), "Errors occured during MSMQ create verification.");
        }

    }
}