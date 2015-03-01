using System;
using sctp_client;
using sctp_client.Commands;
using sctp_client.Responses;
using sctp_client.Arguments;
using System.Diagnostics;
using System.Collections.Generic;


namespace client_demo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			CommandPool pool = new CommandPool ("127.0.0.1", 55770, ClientType.SyncClient);


			List<ScAddress> adresses = new List<ScAddress> ();
			Console.WriteLine ("Start to create 10000 nodes");
			Stopwatch watch = new  Stopwatch ();
			watch.Start ();

			CommandPool pool = new CommandPool ("127.0.0.1", 55770, ClientType.SyncClient);
			for (int i=0; i<10000; i++) {
				ACommand cmd_create_node = Command.CreateNode (ElementType.sc_type_node_const);
				pool.Send (cmd_create_node);
				adresses.Add ((cmd_create_node.Response as RspCreateNode).CreatedNodeAddress);
			}

			foreach (ScAddress adr in adresses ){
				ACommand cmd_gettype = Command.GetElementType (adr);
				pool.Send (cmd_gettype);

				Console.WriteLine ("Type of Node is: {0}", (cmd_gettype.Response as RspGetElementType).ElementType.ToString ());
			}



			watch.Stop ();
			Console.WriteLine ("Times elapsed: {0}", watch.Elapsed.ToString());
		}
	}
}
