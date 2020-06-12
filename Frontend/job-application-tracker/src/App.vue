<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-btn depressed color="primary" @click="drawer = true">
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
        @click="homepage"
          alt="Job Application Tracker"
          class="shrink mt-1 hidden-sm-and-down"
          contain
          min-width="100"
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-name-dark.png"
          width="100"
        />
      </div>

      <v-spacer></v-spacer>
        <v-menu v-if=isLoggedIn offset-y>
          <template v-slot:activator="{ on, attrs }">
            <v-btn depressed color="primary" v-bind="attrs" v-on="on">
              <v-icon>mdi-chevron-down</v-icon>
            </v-btn>
          </template>
          <v-list>
            <v-list-item v-for="(item, index) in dropDownMenuLinks" :key="index">
              <v-list-item-title>{{ item.title }}</v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" app floating temporary>
      <v-list-item v-if=isLoggedIn>
        <v-list-item-avatar>
          <v-img src="https://randomuser.me/api/portraits/men/78.jpg"></v-img>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>John Leider</v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-item v-for="item in navigationDrawerLinks" :key="item.title" :to="item.link" link>
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-content>
      <router-view></router-view>
    </v-content>
  </v-app>
</template>

<script>

export default {
  name: "App",

  components: {
  },

  data: () => ({
    drawer: false,
    isLoggedIn: false,
    navigationDrawerLinks: [
          { title: 'Job Applications Tracker', icon: 'mdi-view-dashboard', link: '/jobapplications' },
          { title: 'Job Postings', icon: 'mdi-smart-card', link: '/jobpostings' },
          { title: 'Job Applications Stats', icon: 'mdi-chart-timeline-variant', link: 'jobapplicationstats' },
          { title: 'Upload Documents', icon: 'mdi-text-box-plus', link: '/documentupload'},
          { title: 'Leave Feedback', icon: 'mdi-message-alert', link: '/feedback' },
          { title: 'About', icon: 'mdi-information', link: '/about'},
        ],
    dropDownMenuLinks: [
        { title: 'Settings' },
        { title: 'Log Out' }
      ]
  }),
  methods: {
    homepage() {
      if(this.isLoggedIn){
        this.$router.push("/jobapplications");
      } else {
        this.$router.push("/");
      }
    }
  }
};
</script>
