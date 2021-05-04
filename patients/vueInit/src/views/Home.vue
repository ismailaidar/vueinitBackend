<template>
  <div>
    <h1 class="text-2xl font-bold">DASHBOARD</h1>
    <hr class="pb-4">
    <div class="cards grid xl:grid-cols-3 md:grid-cols-2 sm:grid-1 gap-4" >
      <router-link :to="{name: 'AllPatients', params: {prop: 'all'}}" v-for="item in 6" :key="item" class="card-link">
        <div class="card text-center border-2" >
        <img src="@/assets/images/people.png" class="mx-auto my-6">
        <div class="border-t-2 p-2 bg-blue-400 text-white text-left grid grid-cols-7">
          <span class="col-span-5 text-center">All patients</span>
          <span class="col-span-1">|</span>
          <span class="col-span-1">254</span>
        </div>
      </div>
      </router-link>
    </div>

    <h1 class="mt-4 text-xl ">SOME TEXT</h1>
    <hr class="py-4">
    <p>
      At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.
    </p>
    <p>
      At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.
    </p>
  </div>
</template>

<script>

  export default {
    name: 'Home',
    data() {
      return {
        cards: [
          {
            category: 'all',
            count: 255,
            text: 'All Patients',
          }
        ]
      }
    },
    components: {
    },
  }
</script>

<style lang="css">
  .card-link {
    border: none !important
  }
</style>