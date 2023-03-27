export * from './information.service';
import { InformationService } from './information.service';
export * from './sentences.service';
import { SentencesService } from './sentences.service';
export * from './wordTypes.service';
import { WordTypesService } from './wordTypes.service';
export * from './words.service';
import { WordsService } from './words.service';
export const APIS = [InformationService, SentencesService, WordTypesService, WordsService];
