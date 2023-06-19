import { Profile } from "./profile";
export interface ActivityChoice {
    id: string;
    title: string;

}
export interface Activity {
    id: string;
    title: string;
    description: string;
    category: string;
    createDate: Date | null;
    //array of choices
    choices: ActivityChoice[];
    closeDate: Date;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing?: boolean;
    isHost?: boolean
    voters: Profile[]
    host?: Profile;
}

export class ActivityFormValues
  {
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    choices: ActivityChoice[] = [];
    closeDate: Date | null = null;

	  constructor(activity?: ActivityFormValues) {
      if (activity) {
        this.id = activity.id;
        this.title = activity.title;
        this.category = activity.category;
        this.description = activity.description;
        this.choices = activity.choices;
        this.closeDate = activity.closeDate;
      }
    }

  }

  export class Activity implements Activity {
    constructor(init?: ActivityFormValues) {
      Object.assign(this, init);
    }
  }
