﻿using MyServer;
using MyServer.div;

ReadConfig config = new ReadConfig();
config.readConfig("../../../div/Config.xml");

myserver server = new myserver(config.ServerPortNr,config.shutDownPortNr, config.ServerNametr);

TryLog log = new TryLog();
log.Start();

server.Start();