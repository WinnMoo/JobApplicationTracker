<template>
  <v-card class="mx-auto">
    <v-container>
      <v-row>
        <v-list-item>
          <v-list-item-content>
            <v-col>
              <v-list-item-title class="display-1 text--primary">{{ jobApplication.company }}</v-list-item-title>
            </v-col>
            <v-col class="text-right">
              <v-list-item-title>
                <v-icon>mdi-map-marker</v-icon>
                {{ jobApplication.location }}
              </v-list-item-title>
            </v-col>
            <v-col cols="10">
              <v-list-item-subtitle>{{ jobApplication.jobTitle }}</v-list-item-subtitle>
            </v-col>
            <v-col cols="2" class="text-right">
              <v-chip :color="this.statuses[jobApplication.status].color" text-color="white"> {{statuses[jobApplication.status].text}}</v-chip>
            </v-col>
            <v-col>
              <v-row justify="space-between">
                <v-col cols="10">
                  <v-card-text>
                    <div class="text--primary">{{ jobApplication.description }}</div>
                  </v-card-text>
                </v-col>
                <v-row class="flex-column ma-0 fill-height" justify="center">
                  <v-col class="text-right">
                    <v-col class="px-0">
                      <v-btn
                        icon
                        color="primary"
                        @click="openUpdateDialog"
                      >
                        <v-icon>mdi-pencil</v-icon>
                      </v-btn>
                    </v-col>
                    <v-col class="px-0">
                      <v-btn icon color="error" @click="deleteDialog = true; indexToDelete = index">
                        <v-icon>mdi-trash-can</v-icon>
                      </v-btn>
                    </v-col>
                  </v-col>
                </v-row>
              </v-row>
            </v-col>
          </v-list-item-content>
        </v-list-item>
      </v-row>
    </v-container>
  </v-card>
</template>

<script>
export default {
  name: "job-application-card",
  props: {
    jobApplication: {
      id: '',
      companyName: '',
      jobTitle: '',
      location: '',
      description: '',
      status: '',
      dateApplied: '',
    }
  },
  data() {
    return {
      statuses: [
        {
          color: "grey",
          text: "Set Status"
        },
        {
          color: "green",
          text: "Accepted"
        },
        {
          color: "orange",
          text: "In Progress"
        },
        {
          color: "error",
          text: "Rejected"
        }
      ]
    };
  },
  methods: {
    openUpdateDialog: function (){
      console.log("Emitting open update dialog");
      this.$emit('openUpdateDialog', true);
    }
  }
};
</script>