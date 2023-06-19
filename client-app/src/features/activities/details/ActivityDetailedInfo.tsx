import {format} from 'date-fns';
import {observer} from 'mobx-react-lite';
import React from 'react'
import {Grid, Icon, Segment} from 'semantic-ui-react'
import {Activity, ActivityChoice} from "../../../app/models/activity";
import {ArcElement, Chart as ChartJS, Legend, Tooltip} from 'chart.js';
import {Pie} from 'react-chartjs-2';
import {Profile} from "../../../app/models/profile";

ChartJS.register(ArcElement, Tooltip, Legend);
interface Props {
    activity: Activity
}
//data for chart
//getting data from activity.Choices as parameter and passes it into the dataset
var chartJsData = function (resultSet: ActivityChoice[], voters: Profile[]) {
    var labels = [];
    var colors = [];
    const countMap: Record<string, number> = {};

    // Count the occurrences of each title
    voters.forEach((voter) => {
        const choiceId = voter.choiceId;
        countMap[choiceId] = (countMap[choiceId] || 0) + 1;
    });

    const data = resultSet.map((choice) => countMap[choice.id] || 0);
    for (let i = 0; i < resultSet.length; i++) {
        labels.push(resultSet[i].title);
        colors.push('#fff');
    }

    return {
        datasets: [{
        data: data,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
            ],
        }],
        labels: labels
    };
};
export default observer(function ActivityDetailedInfo({ activity }: Props) {
    return (
        <Segment.Group>
            <Segment attached='top'>
                <Grid>
                    <Grid.Column width={1}>
                        <Icon size='large' color='teal' name='info' />
                    </Grid.Column>
                    <Grid.Column width={15}>
                        <p>{activity.description}</p>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='calendar' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={15}>
                        <span>
                            {format(activity.closeDate!, 'dd MMM yyyy h:mm aa')}
                        </span>
                    </Grid.Column>
                </Grid>
            </Segment>
            <Segment attached>
                <Grid verticalAlign='middle'>
                    <Grid.Column width={1}>
                        <Icon name='marker' size='large' color='teal' />
                    </Grid.Column>
                    <Grid.Column width={15}>
                        <Pie data={chartJsData(activity.choices, activity.voters)} />
                    </Grid.Column>
                </Grid>
            </Segment>
        </Segment.Group>
    )
})