interface VirtualMachine {
  status: string;
  vmid: number;
  cpus?: number;
  maxdisk?: number;
  maxmem?: number;
  name?: string;
  tags?: string;
  uptime?: number;
}

export default VirtualMachine;
