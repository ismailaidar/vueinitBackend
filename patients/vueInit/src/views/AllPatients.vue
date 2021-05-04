<template>
  <div data-app>
    <h1 class="text-2xl font-bold flex ">
      <svg class="self-center w-10 h-10 inline-block" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17 20h5v-2a3 3 0 00-5.356-1.857M17 20H7m10 0v-2c0-.656-.126-1.283-.356-1.857M7 20H2v-2a3 3 0 015.356-1.857M7 20v-2c0-.656.126-1.283.356-1.857m0 0a5.002 5.002 0 019.288 0M15 7a3 3 0 11-6 0 3 3 0 016 0zm6 3a2 2 0 11-4 0 2 2 0 014 0zM7 10a2 2 0 11-4 0 2 2 0 014 0z"></path></svg>
      <span class="self-end">ALL PATIENTS</span>
    </h1>
    <hr class="pb-4" />
    <PatientTable />
  </div>
</template>

<script>
import PatientTable from "./../components/AllPatients/PatientTable";
export default {
  data() {
    return {
    };
  },
  components: {
    PatientTable
  }
};
</script>

<style scoped>

</style>

