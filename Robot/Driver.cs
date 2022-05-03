using System;
using Robot;

//integrate commander API and get it to work with the program to steam commands
namespace Driver
{
    class Program
    {
        System.IntPtr HIDHandle;

        public int Driver(string command)
        {
            Console.WriteLine(Connect());
            return MoveCommand(command);
        }
        public int Connect()
        {
            int numberHIDs;
            int retVal;
            string hidNameStr;
            byte[] tempHIDName = new byte[32];
            // Get the number of connected devices.
            numberHIDs = Robot.CMDHID.GetCommanderHIDnumber();

            if (numberHIDs == 0)
            {
                Console.WriteLine("No target was detected");
            }
            unsafe
            {
                // Get the device name of the index that was commanded.
                fixed (void* ptr = &tempHIDName[0])
                {
                    retVal = Robot.CMDHID.GetCommanderHIDName(0, new IntPtr(ptr));
                }
                //Marshal.Copy(tempHIDName, 0, HIDName, 32)
                hidNameStr = System.Text.Encoding.ASCII.GetString(tempHIDName);
                hidNameStr = hidNameStr.TrimEnd('\0');

                if (hidNameStr.Equals("CMD-4CR-01") == false)

                {
                    Console.WriteLine("No CMD-4CR available to open");
                }
                else
                {
                    //Get a device handle and open it (make it operable).
                    retVal = Robot.CMDHID.OpenCommanderHID(out HIDHandle, tempHIDName);
                    if (retVal == 0)
                    {
                        Console.WriteLine("Error opening device");
                        // ƒClose the device
                        retVal = Robot.CMDHID.CloseCommanderHID(HIDHandle);
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("CMD-4CR opened!!");
                    }
                }
            }
            return 1;
        }

        public int MoveCommand(string command)
        {
            int retVal = 0;
            byte[] replyBStr = new byte[64];
            byte[] sendBStr = new byte[64];
            string replyStr;
            string sendStr;
            unsafe
            {
                sendStr = command;
                sendBStr = System.Text.Encoding.ASCII.GetBytes(sendStr);

                fixed (void* ptrSnd = &sendBStr[0], ptrRpy = &replyBStr[0])
                {
                    retVal = Robot.CMDHID.SendReceiveCommanderHID(HIDHandle, new IntPtr(ptrSnd), new IntPtr(ptrRpy));
                }

                if (retVal == 0)
                {
                    Console.WriteLine("SendReceive Error!", retVal.ToString());
                    return 0;
                }
                replyStr = System.Text.Encoding.ASCII.GetString(replyBStr);
                Console.WriteLine(command, " : ", replyStr);
            }
            return 1;
        }

        public int CloseConnect()
        {
            int retVal = Robot.CMDHID.CloseCommanderHID(HIDHandle);
            Console.WriteLine("Connection closed.");
            return 1;
        }


    }
}