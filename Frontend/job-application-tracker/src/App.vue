<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <div class="d-flex align-center">
        <v-btn
          v-if="isLoggedIn"
          depressed
          color="primary"
          @click="drawer = true"
        >
          <v-icon>mdi-menu</v-icon>
        </v-btn>
        <v-img
          :style="{ cursor: 'pointer' }"
          @click="homepage"
          alt="Vuetify Logo"
          class="shrink mr-2"
          contain
          src="https://cdn.vuetifyjs.com/images/logos/vuetify-logo-dark.png"
          transition="scale-transition"
          width="40"
        />

        <v-img
          :style="{ cursor: 'pointer' }"
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
      <v-menu offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            v-show="isLoggedIn"
            depressed
            color="primary"
            v-bind="attrs"
            v-on="on"
          >
            <v-icon>mdi-chevron-down</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item
            v-for="(item, index) in dropDownMenuLinks"
            :key="index"
            @click="DropDownMenuClick(item.title)"
          >
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-navigation-drawer v-model="drawer" app floating temporary>
      <v-list-item>
        <v-list-item-avatar>
          <v-avatar color="indigo" size="36">
            <span class="white--text headline">{{ firstName }}</span>
          </v-avatar>
        </v-list-item-avatar>

        <v-list-item-content>
          <v-list-item-title>{{ emailAddress }}</v-list-item-title>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense>
        <v-list-item
          v-for="item in navigationDrawerLinks"
          :key="item.title"
          :to="item.link"
          link
        >
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
export default {
  name: "App",
  components: {},
  data() {
    return {
      drawer: false,
      navigationDrawerLinks: [
        {
          title: "Job Applications Tracker",
          icon: "mdi-view-dashboard",
          link: "/jobapplications"
        },
        { title: "Job Postings", icon: "mdi-smart-card", link: "/jobpostings" },
        {
          title: "Job Applications Stats",
          icon: "mdi-chart-timeline-variant",
          link: "jobapplicationstats"
        },
        {
          title: "Upload Documents",
          icon: "mdi-text-box-plus",
          link: "/documentupload"
        },
        {
          title: "Leave Feedback",
          icon: "mdi-message-alert",
          link: "/feedback"
        },
        { title: "About", icon: "mdi-information", link: "/about" }
      ],
      dropDownMenuLinks: [{ title: "Settings" }, { title: "Log Out" }]
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.getters.isLoggedIn;
    },
    firstName() {
      if (this.$store.getters.firstName == null) {
        return null;
      } else {
        return this.$store.getters.firstName.substring(0, 1);
      }
    },
    emailAddress() {
      if (this.$store.getters.emailAddress == null) {
        return null;
      } else {
        return this.$store.getters.emailAddress.toLowerCase();
      }
    }
  },
  methods: {
    homepage: function() {
      if (this.$store.getters.isLoggedIn) {
        this.$router.push("/dashboard");
      } else {
        this.$router.push("/");
      }
    },
    DropDownMenuClick: function(title) {
      if (title === "Settings") {
        this.$router.push("/settings");
      }
      if (title === "Log Out") {
        this.Logout();
      }
    },
    Logout: function() {
      axios({
        method: "POST",
        url: `${apiURL}/session/` + "logout",
        data: {
          JWTToken: localStorage.getItem("jwtToken")
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          return response;
        })
        .catch(e => {
          this.formErrorMessage = e.response.data;
        })
        .finally(() => {
          this.$store.dispatch("logOut");
          this.$store.dispatch("removeEmailAddress");
          this.$store.dispatch("removeFirstName");
          localStorage.removeItem("jwtToken");
          this.$router.push("/");
          this.$forceUpdate();
        });
    }
  }
};
</script>
