import { defineStore } from "pinia";
import { ref } from "vue";
import { FetchNodeNames } from "@api/virtualMachines/fetchNodes";
import Lxc from "@/models/lxc";
import Qemu from "@/models/qemu";

export const useVmStore = defineStore("virtual-machine", () => {
  const qemuVirtualMachines = ref<Qemu[]>([]);
  const lxcVirtualMachines = ref<Lxc[]>([]);
  const nodes = ref<string[]>([]);

  async function fetchNodes() {
    const result: Array<string> = await FetchNodeNames();
    nodes.value = result;
  }

  return {
    qemuVirtualMachines,
    lxcVirtualMachines,
    nodes,
    fetchNodes,
  };
});
