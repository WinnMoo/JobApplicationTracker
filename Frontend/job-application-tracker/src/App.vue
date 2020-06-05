<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-btn depressed color="primary">
          <v-icon>mdi-menu</v-icon>
        </v-btn>
        <v-img
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />

        <v-img
          alt="Job Application Tracker"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>

      <v-spacer></v-spacer>

      <v-btn depressed color="primary">
        <v-icon>mdi-chevron-down</v-icon>
      </v-btn>
    </v-app-bar>

    <v-content>
      <JobApplication />
    </v-content>
  </v-app>
</template>

<script>
import JobApplication from "./components/JobApplication";

export default {
  name: "App",

  components: {
    JobApplication
  },

  data: () => ({
    //
  })
};
</script>
