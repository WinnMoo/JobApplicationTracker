<template>
  <v-row dense>
    <v-col>
      <v-card style="position: sticky">
        <v-row>
          <v-col v-for="column in columns" :key="column.text">
            {{ column.text }}
          </v-col>
        </v-row>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
export default {
  name: "JobApplicationDenseInfo",
  data() {
    return {
      columns: [
        { text: "Company Name" },
        { text: "Job Title" },
        { text: "Description" },
        { text: "Date Applied" },
        { text: "Location" },
        { text: "Actions" }
      ]
    };
  }
};
</script>
<style>
#fixed {
  position: sticky;
}
</style>
