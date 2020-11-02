<template>
  <v-container fluid>
    <v-card class="mx-auto text-center" dark>
      <v-card-title class="display-1 font-weight-thin justify-center"
        >Your Application Statistics</v-card-title
      >
      <v-col align-self="center">
        <v-select
          label="Filter by time"
          :items="lengthOfTimeFilter"
          dark
          v-model="lengthOfTime"
        ></v-select>
      </v-col>
      <v-card>
        <v-card-title class="justify-center">
          <div class="display-1 font-weight-thin">
            Job Applications within the past {{ lengthOfTime }}
          </div>
        </v-card-title>
        <v-tabs v-model="tab" dark grow>
          <v-tab v-for="item in items" :key="item.tab">{{ item.tab }}</v-tab>
          <v-tab-item>
            <v-card dark>
              <v-card-text></v-card-text>
            </v-card>
          </v-tab-item>
          <v-tab-item>
            <v-card dark>
              <v-card-text>
                <vue-funnel-graph
                  :width="width"
                  :height="height"
                  :labels="labels"
                  :values="values"
                  :direction="direction"
                  :gradient-direction="gradientDirection"
                  :animated="true"
                ></vue-funnel-graph>
              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs>
      </v-card>
    </v-card>
  </v-container>
</template>

<script>
import axios from "axios";
import { apiURL } from "@/const.js";
import { VueFunnelGraph } from "vue-funnel-graph-js";
export default {
  name: "JobApplicationStats",
  components: {
    VueFunnelGraph
  },
  data() {
    return {
      lengthOfTime: "Week",
      fill: true,
      padding: 8,
      radius: 10,
      value: [0, 2, 5, 9, 5, 10, 3, 5, 0, 0, 1, 8, 2, 9, 0],
      widthSparkline: 2,
      gradients: [
        ["#222"],
        ["#42b3f4"],
        ["red", "orange", "yellow"],
        ["purple", "violet"],
        ["#00c6ff", "#F0F", "#FF0"],
        ["#f72047", "#ffd200", "#1feaea"]
      ],
      tab: null,
      items: [
        { tab: "Number of Applications Over Time", content: "Tab 1 Content" },
        { tab: "Applications Statuses", content: "Tab 2 Content" }
      ],
      labels: ["Applications Sent", "In Progress", "Accepted"],
      values: [],
      lengthOfTimeFilter: ["Week", "Month", "6 Months", "Year"],
      direction: "horizontal",
      gradientDirection: "horizontal",
      height: 300,
      width: 800
    };
  },
  methods: {
    fetchFunnelGraphStats: function() {
      let lengthOfTimeFilterIndex = this.lengthOfTimeFilter.findIndex(
        element => element == this.lengthOfTime
      );
      axios({
        method: "GET",
        url: `${apiURL}/jobapp/` + "getfunnelgraphstats",
        params: {
          LengthOfTime: lengthOfTimeFilterIndex,
          EmailAddress: this.$store.getters.emailAddress
        },
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Credentials": true
        }
      })
        .then(response => {
          this.values = response.data;
        })
        .catch(e => {
          console.log(e);
        });
    }
  },
  created: function() {
    this.fetchFunnelGraphStats();
  },
  watch: {
    lengthOfTime: function() {
      this.fetchFunnelGraphStats();
    }
  }
};
</script>
