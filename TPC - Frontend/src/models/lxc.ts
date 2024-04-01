import VirtualMachine from "./virtualMachine";

interface Lxc extends VirtualMachine {
  maxswap?: number;
}

export default Lxc;
