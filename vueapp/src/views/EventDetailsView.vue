<template>
    <EventData></EventData>
    <div class="row" style="margin-top: 30px;">
        <ParticipantForm @participantAdded="participantsUpdated = !participantsUpdated"></ParticipantForm>
        <ParticipantList :participantsUpdated="participantsUpdated"></ParticipantList>
    </div>
</template>

<script setup lang="ts">
    import EventData from '../components/EventData.vue';
    import ParticipantForm from '../components/ParticipantForm.vue';
    import ParticipantList from '../components/ParticipantList.vue';
    import { ref } from 'vue';

    const participantsUpdated = ref(false);
</script>