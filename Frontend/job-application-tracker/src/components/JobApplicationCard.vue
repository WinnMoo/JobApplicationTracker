<template>
  <v-row dense>
    <v-col cols="12">
      <v-card @click="resizeCard()" v-bind:style="{ 'max-height': cardHeight }">
        <v-container>
          <v-row>
            <v-list-item>
              <v-list-item-content>
                <v-col>
                  <v-list-item-title class="display-1 text--primary">
                    <div v-if="this.jobApplication.jobPostingURL == null">
                      {{ jobApplication.companyName }}
                    </div>
                    <div v-if="this.jobApplication.jobPostingURL != null">
                      <a
                        target="_blank"
                        rel="noopener noreferrer"
                        v-bind:href="this.jobApplication.jobPostingURL"
                      >
                        {{ jobApplication.companyName }}
                      </a>
                    </div>
                  </v-list-item-title>
                </v-col>
                <v-col class="text-right">
                  <v-list-item-title>
                    <v-menu
                      offset-x
                      offset-y
                      rounded="xl"
                      transition="slide-x-transition"
                      allow-overflow
                      :close-on-content-click="false"
                      v-model="locationMenu"
                    >
                      <template v-slot:activator="{ on }">
                        <div
                          v-show="
                            jobApplication.city != null &&
                              jobApplication.state != null
                          "
                        >
                          <v-icon v-on="on" @click="!locationMenu"
                            >mdi-map-marker</v-icon
                          >
                          {{
                            jobApplication.city + ", " + jobApplication.state
                          }}
                        </div>
                        <div
                          v-show="
                            jobApplication.city == null &&
                              jobApplication.state != null
                          "
                        >
                          <v-icon v-on="on" @click="!locationMenu"
                            >mdi-map-marker</v-icon
                          >
                          {{ jobApplication.state }}
                        </div>
                        <div
                          v-show="
                            jobApplication.city != null &&
                              jobApplication.state == null
                          "
                        >
                          <v-icon v-on="on" @click="!locationMenu"
                            >mdi-map-marker</v-icon
                          >
                          {{ jobApplication.city }}
                        </div>
                        <div
                          v-show="
                            jobApplication.city == null &&
                              jobApplication.state == null
                          "
                        >
                          <v-icon v-on="on" @click="!locationMenu"
                            >mdi-map-marker</v-icon
                          >
                          Set location using the map marker
                        </div>
                      </template>
                      <v-card elevation="0">
                        <v-card-subtitle>
                          <v-row>
                            <v-col>
                              <v-autocomplete
                                v-model="selectedState"
                                :items="states"
                                color="primary"
                                background-color="white"
                                item-text="Description"
                                label="State"
                                placeholder="Start typing to Search"
                                return-object
                              ></v-autocomplete>
                            </v-col>
                            <v-col>
                              <v-autocomplete
                                v-model="selectedCity"
                                :items="cities"
                                color="primary"
                                background-color="white"
                                hide-no-data
                                hide-selected
                                item-text="Description"
                                label="City"
                                placeholder="Start typing to Search"
                                return-object
                              ></v-autocomplete>
                            </v-col>
                          </v-row>
                          <v-row align="center" justify="center">
                            <v-btn color="primary" @click="saveLocation"
                              >Save</v-btn
                            >
                          </v-row>
                        </v-card-subtitle>
                      </v-card>
                    </v-menu>
                  </v-list-item-title>
                </v-col>
                <v-col cols="10">
                  <v-list-item-subtitle>{{
                    jobApplication.jobTitle
                  }}</v-list-item-subtitle>
                </v-col>
                <v-col cols="2" class="text-right">
                  <v-menu
                    offset-x
                    close-on-click
                    open-on-hover
                    rounded="xl"
                    transition="slide-x-transition"
                  >
                    <template v-slot:activator="{ on }">
                      <v-chip
                        v-on="on"
                        :color="statuses[jobApplication.status].color"
                        text-color="white"
                        >{{ statuses[jobApplication.status].text }}</v-chip
                      >
                    </template>
                    <v-card>
                      <v-list>
                        <v-list-item
                          v-for="item in statuses.slice(1)"
                          :key="item.color"
                        >
                          <v-chip
                            :color="item.color"
                            text-color="white"
                            @click="selectStatus(item.status)"
                            >{{ item.text }}</v-chip
                          >
                        </v-list-item>
                      </v-list>
                    </v-card>
                  </v-menu>
                </v-col>
                <v-col>
                  <v-row justify="space-between">
                    <v-col cols="10">
                      <v-card-text
                        v-bind:style="{
                          height: descriptionHeight,
                          overflow: 'hidden',
                          'word-break': 'break-word'
                        }"
                      >
                        <div class="text--primary">
                          {{ jobApplication.description }}
                        </div>
                      </v-card-text>
                    </v-col>
                    <v-row
                      class="flex-column ma-0 fill-height"
                      justify="center"
                    >
                      <v-col class="text-right">
                        <v-col class="px-0">
                          <v-btn icon color="primary" @click="openUpdateDialog">
                            <v-icon>mdi-pencil</v-icon>
                          </v-btn>
                        </v-col>
                        <v-col class="px-0">
                          <v-btn icon color="error" @click="openDeleteDialog">
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
    </v-col>
  </v-row>
</template>

<script>
import countrycitystatejson from "countrycitystatejson";

export default {
  name: "JobApplicationCard",
  props: {
    jobApplication: {
      jobApplicationId: "",
      companyName: "",
      jobTitle: "",
      city: "",
      state: "",
      Description: "",
      status: "",
      dateApplied: "",
      jobPostingURL: ""
    }
  },
  data() {
    return {
      cardHeight: "315px",
      descriptionOverflow: "auto",
      descriptionHeight: "100px",
      locationMenu: false,
      selectedState: null,
      selectedCity: null,
      statuses: [
        {
          status: 0,
          color: "grey",
          text: "Set Status"
        },
        {
          status: 1,
          color: "primary",
          text: "Applied"
        },
        {
          status: 2,
          color: "orange",
          text: "In Progress"
        },
        {
          status: 3,
          color: "error",
          text: "Rejected"
        },
        {
          status: 4,
          color: "green",
          text: "Accepted"
        }
      ],
      states: [
        "Alabama",
        "Alaska",
        "Arizona",
        "Arkansas",
        "California",
        "Colorado",
        "Connecticut",
        "Delaware",
        "District of Columbia",
        "Florida",
        "Georgia",
        "Hawaii",
        "Idaho",
        "Illinois",
        "Indiana",
        "Iowa",
        "Kansas",
        "Kentucky",
        "Louisiana",
        "Maine",
        "Maryland",
        "Massachusetts",
        "Michigan",
        "Minnesota",
        "Mississippi",
        "Missouri",
        "Montana",
        "Nebraska",
        "Nevada",
        "New Hampshire",
        "New Jersey",
        "New Mexico",
        "New York",
        "North Carolina",
        "North Dakota",
        "Ohio",
        "Oklahoma",
        "Oregon",
        "Pennsylvania",
        "Rhode Island",
        "South Carolina",
        "South Dakota",
        "Tennessee",
        "Texas",
        "Utah",
        "Vermont",
        "Virgin Island",
        "Virginia",
        "Washington",
        "West Virginia",
        "Wisconsin",
        "Wyoming"
      ],
      cities: []
    };
  },
  watch: {
    selectedState: function() {
      this.cities = countrycitystatejson.getCities("US", this.selectedState);
    }
  },
  methods: {
    selectStatus: function(status) {
      if (status !== 0) {
        this.$emit(
          "updateStatus",
          status,
          this.jobApplication.jobApplicationId
        );
      }
    },
    openUpdateDialog: function() {
      this.$emit(
        "openUpdateDialog",
        true,
        this.jobApplication.jobApplicationId
      );
    },
    openDeleteDialog: function() {
      this.$emit(
        "openDeleteDialog",
        true,
        this.jobApplication.jobApplicationId
      );
    },
    saveLocation: function() {
      this.$emit(
        "updateLocation",
        this.selectedCity,
        this.selectedState,
        this.jobApplication.jobApplicationId
      );
      this.locationMenu = false;
      this.$forceUpdate;
    },
    resizeCard: function() {
      if (this.descriptionHeight == "100px") {
        this.cardHeight = "100%";
        this.descriptionHeight = "100%";
      } else {
        this.descriptionHeight = "100px";
        this.cardHeight = "315px";
      }
    }
  }
};
</script>
