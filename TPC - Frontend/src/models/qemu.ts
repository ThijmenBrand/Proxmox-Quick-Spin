import VirtualMachine from "./virtualMachine";

interface Qemu extends VirtualMachine {
  pid?: number;
  qmpstatus?: string;
  running_machine?: string;
  running_qemu?: string;
}

export default Qemu;
