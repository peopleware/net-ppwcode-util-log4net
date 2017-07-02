// Copyright 2017 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Globalization;
using System.IO;
using System.Text;

using Amazon;
using Amazon.S3.Transfer;

using log4net.Appender;
using log4net.Core;

namespace PPWCode.Util.Log4Net.I
{
    /// <summary>
    ///     Class that places the logs on an S3 bucket.
    /// </summary>
    public class S3Appender : BufferingAppenderSkeleton
    {
        private string m_BucketName;
        private string m_KeyPrefix;
        private string m_Region;

        public S3Appender()
        {
        }

        public string Region
        {
            get { return m_Region; }
            set { m_Region = value; }
        }

        public string BucketName
        {
            get { return m_BucketName; }
            set { m_BucketName = value; }
        }

        public string KeyPrefix
        {
            get { return m_KeyPrefix; }
            set { m_KeyPrefix = value; }
        }

        protected override bool RequiresLayout
        {
            get { return true; }
        }

        protected override void SendBuffer(LoggingEvent[] events)
        {
            try
            {
                StringWriter writer = new StringWriter(CultureInfo.InvariantCulture);

                string t = Layout.Header;
                if (t != null)
                {
                    writer.Write(t);
                }

                foreach (LoggingEvent @event in events)
                {
                    RenderLoggingEvent(writer, @event);
                }

                t = Layout.Footer;
                if (t != null)
                {
                    writer.Write(t);
                }

                SendToS3(writer.ToString());
            }
            catch (Exception e)
            {
                ErrorHandler.Error("An error occurred while sending event logs to S3.", e);
            }
        }

        private void SendToS3(string content)
        {
            string key = string.Format("{0}_{1:yyyy-MM-dd_HH-mm-ss_fff}", KeyPrefix, DateTime.UtcNow);
            RegionEndpoint region = RegionEndpoint.GetBySystemName(Region);
            using (ITransferUtility utility = new TransferUtility(region))
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(content ?? string.Empty)))
            {
                utility.Upload(stream, BucketName, key);
            }
        }
    }
} 