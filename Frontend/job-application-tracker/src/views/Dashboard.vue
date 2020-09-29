<template>
  <v-container fluid>
    <v-row dense>
      <v-col cols="12">
        <v-card class="elevation-12">
          <v-toolbar color="primary" dark flat dense>
            <v-toolbar-title>Welcome</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-card class="elevation-12">
              <v-toolbar color="primary" dark flat dense>
                <v-toolbar-title>Jobs you may be interested in</v-toolbar-title>
              </v-toolbar>
            </v-card>
          </v-card-text>
        </v-card>
      </v-col>
      <v-col cols="6">
        <v-card class="elevation-12">
          <v-toolbar color="primary" dark flat dense>
            <v-toolbar-title>Global Stats</v-toolbar-title>
          </v-toolbar>
          <v-card-text></v-card-text>
        </v-card>
      </v-col>
      <v-col cols="6">
        <v-card class="elevation-12" max-height="350px">
          <v-toolbar color="primary" dark flat dense>
            <v-toolbar-title>Your weekly stats</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <JobApplicationsLineChart
              :chart-data="datacollection"
              :styles="this.styles"
              :options="this.options"
            ></JobApplicationsLineChart>
            <button @click="fillData()">Randomize</button>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import JobApplicationsLineChart from "@/components/JobApplicationsLineChart.vue";

export default {
  components: {
    JobApplicationsLineChart
  },
  name: "Dashboard",
  data() {
    return {
      datacollection: null,
      options: {
        scales: {
          yAxes: [
            {
              ticks: {
                beginAtZero: true
              }
            }
          ]
        },
        responsive: true,
        maintainAspectRatio: false
      },
      styles: {
        height: "280px"
      }
    };
  },
  methods: {
    fillData() {
      this.datacollection = {
        labels: [this.getRandomInt(), this.getRandomInt()],
        datasets: [
          {
            label: "Data One",
            backgroundColor: "#f87979",
            data: [this.getRandomInt(), this.getRandomInt()]
          },
          {
            label: "Data One",
            backgroundColor: "#f87979",
            data: [this.getRandomInt(), this.getRandomInt()]
          }
        ]
      };
    },
    getRandomInt() {
      return Math.floor(Math.random() * (50 - 5 + 1)) + 5;
    }
  }
};
</script>
<style>
</style>