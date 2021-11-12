﻿using System.Diagnostics;
using System.IO;
using System.Text;

namespace ntlmrelaynet.Commands {
    public class RelayRequest : BitseryObject {

        public ulong Context { get; private set; }

        public ulong CredentialHandle { get; private set; }

        public int ProcessID { get; private set; }
        public byte[] Token { get; private set; }
        public Process Process { get; private set; }

        public RelayRequest(ulong context, int processID, byte[] token) {
            Context = context;
            ProcessID = processID;
            Token = token;
            Process = Process.GetProcessById(ProcessID);
        }

        public RelayRequest(Stream source) {
            using (var reader = new BitseryReader(source, Encoding.Unicode, true)) {
                Context = reader.ReadUInt64();
                CredentialHandle = reader.ReadUInt64();
                ProcessID = reader.ReadInt32();
                Token = reader.ReadPrefixedBytes();
                Process = Process.GetProcessById(ProcessID);
            }
        }

        public override void Write(BitseryWriter writer) {
            throw new System.NotImplementedException();
        }
    }
}
