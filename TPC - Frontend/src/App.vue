<script setup lang="ts">
import { useVmStore } from "@store/vmStore";
import { computed, onMounted } from "vue";

const store = useVmStore();

const nodes = computed(() => store.nodes);

onMounted(() => {
  store.fetchNodes();
});
</script>

<template>
    <div>
        <h1>Nodes</h1>
        <ul>
        <li v-for="node in nodes" :key="node">
            {{ node }}
        </li>
        </ul>
    </div>
</template>

<style scoped>

</style>
